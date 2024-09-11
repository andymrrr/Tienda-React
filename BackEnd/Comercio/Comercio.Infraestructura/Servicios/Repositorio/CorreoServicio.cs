using Comercio.Aplicacion.Modelo.Correo;
using Comercio.Aplicacion.Servicios.Interfaz;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Comercio.Infraestructura.Servicios.Repositorio
{
    public class CorreoServicio : ICorreoServicio
    {
        public ConfiguracionCorreo _ConfiguracionCorreo { get; }
        public ILogger<CorreoServicio> _logger { get; }
        public CorreoServicio(IOptions<ConfiguracionCorreo> configuracionCorreo, ILogger<CorreoServicio> logger)
        {
            _ConfiguracionCorreo = configuracionCorreo.Value;
            _logger = logger;
        }
        public bool EnviarMensaje(MensajeCorreo correo, string token)
        {
            try
            {
                var desdeemail = _ConfiguracionCorreo.Nombre;
                var pass = _ConfiguracionCorreo.Password;
                var mensaje = new MailMessage();
                mensaje.From = new MailAddress(desdeemail!);
                mensaje.Subject = correo.Titulo;
                mensaje.To.Add(new MailAddress(correo.Para!));
                mensaje.Body = $"{correo.Cuerpo} {_ConfiguracionCorreo.Web}/contrasena/reiniciar/{token}";
                mensaje.IsBodyHtml = true;
                

                var clienteSmtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = _ConfiguracionCorreo.Puerto,
                    Credentials = new NetworkCredential(desdeemail, pass),
                    EnableSsl = true
                };
                clienteSmtp.Send(mensaje);
               
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo enviar el correo ", ex);
                return false;
            }
        }
    }
}
