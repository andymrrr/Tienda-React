using Comercio.Aplicacion.Excepciones;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuarioEstatus
{
    public class ActualizarAdminUsuarioEstatusHandler : IRequestHandler<ActualizarAdminUsuarioEstatusComando, Usuario>
    {
        private readonly UserManager<Usuario> _userManager;
        public ActualizarAdminUsuarioEstatusHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Usuario> Handle(ActualizarAdminUsuarioEstatusComando request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.Id!);
            if (usuario is null)
            {
                throw new BadRequestException("Usuario no Existe");
            }
            usuario.Activo = !usuario.Activo;

            var resultado = await _userManager.UpdateAsync(usuario);
            if (!resultado.Succeeded)
            {
                throw new BadRequestException("Hubo un error all actualizar el usuario");
            }
            return usuario;
        }
    }
}
