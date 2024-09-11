using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorNombreUsuario
{
    public class BuscarUsuarioPorNombreUsuarioHandler : IRequestHandler<BuscarUsuarioPorNombreUsuarioConsulta, AutenticacionRespuesta>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IIdentidadServicio _identidadServicio;
        public BuscarUsuarioPorNombreUsuarioHandler(UserManager<Usuario> userManager, IIdentidadServicio identidadServicio)
        {
                _identidadServicio = identidadServicio;
            _userManager = userManager; 
        }
        public async Task<AutenticacionRespuesta> Handle(BuscarUsuarioPorNombreUsuarioConsulta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(request.NombreUsuario!);

            if (usuario is null)
            {
                throw new Exception("El Usuario no Existe");
            }

           
            var role = await _userManager.GetRolesAsync(usuario);
            return new AutenticacionRespuesta { 
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Telefono = usuario.Telefono,
                Roles = role,
                Avatar = usuario.AvatarUrl
            };


        }
    }
}
