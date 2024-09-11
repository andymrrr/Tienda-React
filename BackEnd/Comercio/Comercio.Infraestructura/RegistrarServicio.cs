using Comercio.Aplicacion.Modelo.Correo;
using Comercio.Aplicacion.Modelo.Imagen;
using Comercio.Aplicacion.Modelo.Pago;
using Comercio.Aplicacion.Modelo.Token;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Aplicacion.Servicios.Interfaz;
using Comercio.Infraestructura.Persistencia.Repositorios;
using Comercio.Infraestructura.Servicios.Identidad;
using Comercio.Infraestructura.Servicios.Repositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura
{
    public static class RegistrarServicio
    {
        public static IServiceCollection AddServicioInfraestructura(this IServiceCollection servicio, IConfiguration configuracion)
        {
            servicio.AddScoped<IComercioUoW, ComercioUoW>();
            servicio.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
            servicio.AddTransient<ICorreoServicio,CorreoServicio>();
            servicio.AddTransient<IIdentidadServicio, IdentidadServicio>();
            servicio.AddScoped<IGestorImagenServicio, GestorImagenServicio>();

            servicio.Configure<ConfiguracionJwt>(configuracion.GetSection("ConfiguracionJwt"));
            servicio.Configure<ConfiguracionCorreo>(configuracion.GetSection("ConfiguracionCorreo"));
            servicio.Configure<ConfiguracionCloudinary>(configuracion.GetSection("ConfiguracionCloudinary"));
            servicio.Configure<ConfiguracionStripe>(configuracion.GetSection("ConfiguracionStripe"));
            return servicio;
        }
    }
}
