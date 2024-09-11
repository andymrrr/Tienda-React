using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Aplicacion.Excepciones;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.RegistroUsuario
{
    public class RegistroUsuarioComandoHandler : IRequestHandler<RegistroUsuarioComando, AutenticacionRespuesta>
    {
        public readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentidadServicio _identidadServicio;
        public RegistroUsuarioComandoHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IIdentidadServicio identidadServicio)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identidadServicio = identidadServicio;

        }
        public async Task<AutenticacionRespuesta> Handle(RegistroUsuarioComando request, CancellationToken cancellationToken)
        {

            var existeCorreo = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;
            if (existeCorreo)
            {
                throw new BadRequestException("El Email del Usuario ya existe ");
            }
            var existeNombreUsuario = await _userManager.FindByNameAsync(request.UserName!) is null ? false : true;
            if (existeNombreUsuario)
            {
                throw new BadRequestException("El Nombre del Usuario ya existe ");
            }
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                Telefono = request.Telefono,
                UserName = request.UserName,
                AvatarUrl = request.FotoUrl,

            };


            var resultado = await _userManager.CreateAsync(usuario, request.Password!);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, AppRoles.Usuario);

                var rolusuario = await _userManager.GetRolesAsync(usuario);

                return new AutenticacionRespuesta
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    Username = usuario.UserName,
                    Avatar = usuario.AvatarUrl,
                    Token = _identidadServicio.CrearToken(usuario, rolusuario),
                    Roles = rolusuario
                };
            }
            else
            {
                throw new Exception("No se pudo Registrar el Usuario ");
            }

        }
    }
}
