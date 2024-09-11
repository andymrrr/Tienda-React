using Comercio.Aplicacion.Modelo.Correo;
using Comercio.Aplicacion.Servicios.Interfaz;
using Comercio.Dominio.Modelos;
using Comercio.Aplicacion.Excepciones;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.EnviarPassword
{
    public class EnviarPasswordComandoHandler : IRequestHandler<EnviarPasswordComando, string>
    {
        private readonly ICorreoServicio _correoServicio;
        private readonly UserManager<Usuario> _userManager;
        public EnviarPasswordComandoHandler(ICorreoServicio correoServicio, UserManager<Usuario> userManager)
        {
            _correoServicio = correoServicio;
            _userManager = userManager;
        }
        public async Task<string> Handle(EnviarPasswordComando request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            if (usuario is null)
            {
                throw new BadRequestException("El Usuario No Existe");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

            var bytesTextoPlano = Encoding.UTF8.GetBytes(token);
            token = Convert.ToBase64String(bytesTextoPlano);

            var mensaje = new MensajeCorreo { 
                Para = request.Email,
                Cuerpo= $"Resetear El Password Aqui: ",
                Titulo = $"Cambiar el Password"
            };

             var envio = _correoServicio.EnviarMensaje(mensaje, token);
            if (!envio)
            {
                throw new Exception("No se pudo enviar el correo");
            }

            return $"Se Envio el email  a la cuenta {request.Email}";
        }
    }
}
