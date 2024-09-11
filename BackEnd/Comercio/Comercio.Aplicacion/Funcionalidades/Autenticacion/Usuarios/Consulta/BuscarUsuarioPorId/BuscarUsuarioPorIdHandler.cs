using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdHandler : IRequestHandler<BuscarUsuarioPorIdConsulta, AutenticacionRespuesta>
    {
        private readonly UserManager<Usuario> _userManager;
        public BuscarUsuarioPorIdHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AutenticacionRespuesta> Handle(BuscarUsuarioPorIdConsulta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.UsuarioId!);
            if (usuario is null)
            {
                throw new Exception("El Usuario no Existe");
            }
            var role = await _userManager.GetRolesAsync(usuario);
            return new AutenticacionRespuesta { 
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Username = usuario.UserName,
                Avatar = usuario.AvatarUrl,
                Roles = role

            };
        }
    }
}
