using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPotToken
{
    public class BuscarUsuarioPotTokenHandler : IRequestHandler<BuscarUsuarioPotTokenConsulta, AutenticacionRespuesta>
    {
        private readonly IIdentidadServicio _identidadServicio;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly IComercioUoW _comercio;
        public BuscarUsuarioPotTokenHandler(IIdentidadServicio identidadServicio, UserManager<Usuario> userManager, IMapper mapper, IComercioUoW comercio)
        {
            _identidadServicio = identidadServicio;
            _userManager = userManager;
            _mapper = mapper;
            _comercio = comercio;
        }
        public async Task<AutenticacionRespuesta> Handle(BuscarUsuarioPotTokenConsulta request, CancellationToken cancellationToken)
        {
            var usuario = await _userManager.FindByNameAsync(_identidadServicio.ObtenerUsuarioSesion());
            if (usuario is null)
            {
                throw new Exception("No se encontro usuario");
            }
            if (!usuario.Activo)
            {
                throw new Exception("El Usuario esta bloqueado");
            }
            var direccion = await _comercio.Repositorio<Direccion>().BuscarEntidadAsincrono(u => u.Usuario == usuario.UserName);
            var direccionVm = _mapper.Map<DireccionVm>(direccion);
            var roles = await _userManager.GetRolesAsync(usuario);
            var token =  _identidadServicio.CrearToken(usuario, roles);
            return new AutenticacionRespuesta
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Avatar = usuario.AvatarUrl,
                DireccionEnvio = direccionVm,
                Roles = roles,
                Token = token,
                Username = usuario.UserName,


            };
        }
    }
}
