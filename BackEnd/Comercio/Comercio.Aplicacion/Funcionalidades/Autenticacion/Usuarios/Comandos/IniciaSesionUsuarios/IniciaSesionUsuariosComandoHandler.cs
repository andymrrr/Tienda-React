using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
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

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.IniciaSesionUsuarios
{
    public class IniciaSesionUsuariosComandoHandler : IRequestHandler<IniciaSesionUsuariosComando, AutenticacionRespuesta>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentidadServicio _identidadServicio;
        private readonly IMapper _mapeo;
        private readonly IComercioUoW _comercio;
        public IniciaSesionUsuariosComandoHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager, IIdentidadServicio identidadServicio, IComercioUoW comercio, IMapper mapeo)
        {
            _comercio = comercio;
            _mapeo = mapeo;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _identidadServicio = identidadServicio;
        }
        public async Task<AutenticacionRespuesta> Handle(IniciaSesionUsuariosComando request, CancellationToken cancellationToken)
        {

            var usuario = await _userManager.FindByEmailAsync(request.Email!);
            if (usuario is null)
            {
                throw new Exception($"el usuario esta bloqueado contacte al administrador");
            }
            if (!usuario.Activo)
            {
                throw new Exception($"el usuario esta bloqueado contacte al administrador");
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password!, false);

            if (!resultado.Succeeded)
            {
                throw new Exception($"Las Credenciales introducida son erroneas");
            }

            var direccionEnvio = await _comercio.Repositorio<Direccion>().BuscarEntidadAsincrono(b => b.Usuario == usuario.UserName);

            var roles = await _userManager.GetRolesAsync(usuario);

            var tokken = _identidadServicio.CrearToken(usuario, roles);

            var AutenticacionRespuesta = new AutenticacionRespuesta()
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Username = usuario.UserName,
                Avatar = usuario.AvatarUrl,
                DireccionEnvio = _mapeo.Map<DireccionVm>(direccionEnvio),
                Token = tokken,
                Roles = roles

            };

            return AutenticacionRespuesta;

        }
    }
}
