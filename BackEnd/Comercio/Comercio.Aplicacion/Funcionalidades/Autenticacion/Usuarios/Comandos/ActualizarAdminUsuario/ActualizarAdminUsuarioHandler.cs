using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using Comercio.Aplicacion.Excepciones;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuario
{
    internal class ActualizarAdminUsuarioHandler : IRequestHandler<ActualizarAdminUsuarioComando, Usuario>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentidadServicio _identidadServicio;
        public ActualizarAdminUsuarioHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IIdentidadServicio identidadServicio)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identidadServicio = identidadServicio;
        }
        public async Task<Usuario> Handle(ActualizarAdminUsuarioComando request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByIdAsync(request.Id!);
            if (usuario is null) {
                throw new BadRequestException("El Usuario no existe");
            }
            usuario.Nombre = request.Nombre;
            usuario.Apellido = request.Apellido;
            usuario.Telefono = request.Telefono;

            var resultado = await _userManager.UpdateAsync(usuario);
            if(!resultado.Succeeded)
            {
                throw new BadRequestException("El usuario no se pudo actualizar");
            }
            var role = await _roleManager.FindByNameAsync(request.Role!);
            if(role is null)
            {
                throw new BadRequestException("El rol enviado no existe");
            }

            var asignarRol = await _userManager.AddToRoleAsync(usuario,role.Name!);

            return usuario;
        }
    }
}
