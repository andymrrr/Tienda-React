

using AutoMapper;
using Comercio.Aplicacion.Comportamiento;
using Comercio.Aplicacion.Excepciones;
using Comercio.Aplicacion.Mapeo;
using Comercio.Aplicacion.Servicios.Interfaz;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comercio.Infraestructura
{
    public static class RegistrarServicio
    {
        public static IServiceCollection AddServicioAplicacion(this IServiceCollection servicio, IConfiguration configuracion)
        {
            var cofiguracionMapeo = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapeoPerfil());
            });
            IMapper mapeo = cofiguracionMapeo.CreateMapper();
            servicio.AddSingleton(mapeo);
            servicio.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            servicio.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
           
            return servicio;
        }
    }
}
