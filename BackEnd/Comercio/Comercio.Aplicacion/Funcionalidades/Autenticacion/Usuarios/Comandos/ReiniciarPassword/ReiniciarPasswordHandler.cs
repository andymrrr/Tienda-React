using Comercio.Aplicacion.Excepciones;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPassword
{
    public class ReiniciarPasswordHandler : IRequestHandler<ReiniciarPasswordComando>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IIdentidadServicio _identidadServicio;


        public ReiniciarPasswordHandler(UserManager<Usuario> userManager, IIdentidadServicio identidadServicio)
        {
            _userManager = userManager;
            _identidadServicio = identidadServicio;
        }
        public async Task<Unit> Handle(ReiniciarPasswordComando request, CancellationToken cancellationToken)
        {
            var Usuario = await _userManager.FindByNameAsync(_identidadServicio.ObtenerUsuarioSesion());
            if (Usuario is null)
            {
                throw new BadRequestException("El Usuario No Existe");
            }
            var validarContrasenaAnterior =  _userManager.PasswordHasher
                .VerifyHashedPassword(Usuario, Usuario.PasswordHash!, request.AntiguoPassword!);

            if ( !(validarContrasenaAnterior == PasswordVerificationResult.Success))
            {
                throw new BadRequestException("El Pasword anterior el erroneo");
            }

            var nuevaContasenahasheada = _userManager.PasswordHasher.HashPassword(Usuario,request.NuevoPassword!);

            Usuario.PasswordHash = nuevaContasenahasheada;

            var actualizar = await _userManager.UpdateAsync(Usuario);
            if (!actualizar.Succeeded)
            {
                throw new BadRequestException("No se pudo Actualizar el usuario ");
            }

            return Unit.Value;



        }
    }

}
