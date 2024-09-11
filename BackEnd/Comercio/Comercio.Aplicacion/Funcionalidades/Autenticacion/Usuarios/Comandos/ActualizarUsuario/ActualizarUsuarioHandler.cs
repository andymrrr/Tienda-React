using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using Comercio.Aplicacion.Excepciones;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarUsuario
{
    public class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioComando, AutenticacionRespuesta>
    {
        private readonly UserManager<Usuario> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IIdentidadServicio _identidadServicio;
        public ActualizarUsuarioHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IIdentidadServicio identidadServicio)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identidadServicio = identidadServicio;

        }
        public async Task<AutenticacionRespuesta> Handle(ActualizarUsuarioComando request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(_identidadServicio.ObtenerUsuarioSesion());
            if (usuario is null) {
                throw new BadRequestException("El Usuario no Existe");
            }
            usuario.Nombre = request.Nombre;
            usuario.Apellido = request.Apellido;
            usuario.Email = request.Email?? usuario.Email;
            usuario.Telefono = request.Telefono;
            usuario.AvatarUrl = request.FotoUrl ?? usuario.AvatarUrl;

            var actualizar = await _userManager.UpdateAsync(usuario);
            if (!actualizar.Succeeded)
            {
                throw new BadRequestException("Hubo un error Actualizando el Usuario");
            }
            var usuarioPorId = await _userManager.FindByEmailAsync(usuario.Email!);
            var rol = await _userManager.GetRolesAsync(usuarioPorId!);

            return new AutenticacionRespuesta {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Telefono = usuario.Telefono,
                Avatar = usuario.AvatarUrl,
                Token = _identidadServicio.CrearToken(usuarioPorId!,rol),
                Roles = rol,


            };
        }
    }
}
