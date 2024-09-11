using Azure.Core;
using Comercio.Aplicacion.Excepciones;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPasswordToken
{
    public class ReiniciarPasswordTokenHandler : IRequestHandler<ReiniciarPasswordTokenComando, string>
    {
        private readonly UserManager<Usuario> _userManager;
        public ReiniciarPasswordTokenHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Handle(ReiniciarPasswordTokenComando request, CancellationToken cancellationToken)
        {
            if (!string.Equals(request.Password, request.ConfirmarPassword))
            {
                throw new BadRequestException("Contraseña y confirmar Contraseña no son iguales");
            }
            var Usuario = await _userManager.FindByEmailAsync(request.Email!);
            if (Usuario is null)
            {
                throw new BadRequestException("El Email no esta registrado en nuesta base de dato");
            }

            var token = Convert.FromBase64String(request.Token!);
            var resultadotoken = Encoding.UTF8.GetString(token);

            var actualizar = await _userManager.ResetPasswordAsync(Usuario, resultadotoken, request.Password!);

            if (!actualizar.Succeeded)
            {
                throw new Exception("Hubo un error reiniciando el password");
            }

            return $"Se actualizo exitosamente la contraseña {request.Email}";
        }
    }
}
