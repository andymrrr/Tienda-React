using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.MostrarProductoListado;
using Comercio.Dominio.Modelos;
using Comercio.Infraestructura;
using Comercio.Infraestructura.Persistencia;
using Ecommerce.Api.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicioInfraestructura(builder.Configuration);
builder.Services.AddServicioAplicacion(builder.Configuration);

builder.Services.AddDbContext<DbContextComercio>(opcion =>
    opcion.UseSqlServer(builder.Configuration.GetConnectionString("Amazon"),
        b => b.MigrationsAssembly(typeof(DbContextComercio).Assembly.FullName))
);
builder.Services.AddMediatR(typeof(MostrarProductoConsultaHandler).Assembly);
builder.Services.AddControllers(opcion =>
{
    var politica = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opcion.Filters.Add(new AuthorizeFilter(politica));
}).AddJsonOptions(J => J.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//Soporte
IdentityBuilder constructordidentidad = builder.Services.AddIdentityCore<Usuario>();
constructordidentidad = new IdentityBuilder(constructordidentidad.UserType, constructordidentidad.Services);
//Manejo de Roles
constructordidentidad.AddRoles<IdentityRole>().AddDefaultTokenProviders();
//Manejo de los clain
constructordidentidad.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();
constructordidentidad.AddEntityFrameworkStores<DbContextComercio>();
//Login
constructordidentidad.AddSignInManager<SignInManager<Usuario>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opcion =>
{
    opcion.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = llave,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddCors(opcion =>
{
    opcion.AddPolicy("CorsPoolicy", constructor => constructor.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPoolicy");
app.MapControllers();

using (var alcance = app.Services.CreateScope())
{
    var servicio = alcance.ServiceProvider;
    var loggerFactory = servicio.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = servicio.GetRequiredService<DbContextComercio>();
        var usuariomanager = servicio.GetRequiredService<UserManager<Usuario>>();
        var rolemanager = servicio.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.MigrateAsync();
        await DbContextComercioData.CargardatosAsincronos(context, usuariomanager, rolemanager, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en la migracion");
    }
}
app.Run();
