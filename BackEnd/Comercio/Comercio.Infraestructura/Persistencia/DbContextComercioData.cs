using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Dominio.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Persistencia
{
    public class DbContextComercioData
    {
        public static async Task CargardatosAsincronos(DbContextComercio dbContext, UserManager<Usuario> Usuariomanager, RoleManager<IdentityRole> roleManager, ILoggerFactory logger)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                    await roleManager.CreateAsync(new IdentityRole(AppRoles.Usuario));
                }
                if (!Usuariomanager.Users.Any())
                {
                    var usuarioadministrador = new Usuario
                    {
                        Nombre = "Andy",
                        Apellido = "Reyes",
                        Email = "Andymrrr@gmail.com",
                        UserName = "andymrr1993",
                        Telefono = "809229495",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/vaxidrez.jpg?alt=media&token=14a28860-d149-461e-9c25-9774d7ac1b24"
                    };
                    await Usuariomanager.CreateAsync(usuarioadministrador, "Andy963.0123456");
                    await Usuariomanager.AddToRoleAsync(usuarioadministrador, AppRoles.Admin);

                    var usuario = new Usuario
                    {
                        Nombre = "Jose",
                        Apellido = "Canceco",
                        Email = "jocecanceco@gmail.com",
                        UserName = "Josecanceco1993",
                        Telefono = "8092403184",
                        AvatarUrl = "https://firebasestorage.googleapis.com/v0/b/edificacion-app.appspot.com/o/avatar-1.webp?alt=media&token=58da3007-ff21-494d-a85c-25ffa758ff6d"
                    };
                    await Usuariomanager.CreateAsync(usuario, "Andy963.0123456");
                    await Usuariomanager.AddToRoleAsync(usuario, AppRoles.Usuario);
                }

                if (!dbContext.Categorias!.Any())
                {
                    var categoriadatos = File.ReadAllText("../Comercio.Infraestructura/Data/categoria.json");
                    var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriadatos);
                    await dbContext.Categorias.AddRangeAsync(categorias!);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Pais!.Any())
                {
                    var paisdatos = File.ReadAllText("../Comercio.Infraestructura/Data/pais.json");
                    var paises = JsonConvert.DeserializeObject<List<Pais>>(paisdatos);
                    await dbContext.Pais.AddRangeAsync(paises!);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Productos!.Any())
                {
                    var productosdatos = File.ReadAllText("../Comercio.Infraestructura/Data/producto.json");
                    var productos = JsonConvert.DeserializeObject<List<Producto>>(productosdatos);
                    await dbContext.Productos!.AddRangeAsync(productos!);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Imagenes!.Any())
                {
                    var imagendatos = File.ReadAllText("../Comercio.Infraestructura/Data/imagen.json");
                    var imagenes = JsonConvert.DeserializeObject<List<Imagen>>(imagendatos);
                    await dbContext.Imagenes.AddRangeAsync(imagenes!);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Comentarios!.Any())
                {
                    var comentariosdatos = File.ReadAllText("../Comercio.Infraestructura/Data/comentario.json");
                    var comentarios = JsonConvert.DeserializeObject<List<Comentario>>(comentariosdatos);
                    await dbContext.Comentarios!.AddRangeAsync(comentarios!);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<DbContextComercioData>();
                log.LogError(ex.Message, ex.InnerException);
            }
        }
    }
}
