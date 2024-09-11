using Comercio.Aplicacion.Funcionalidades.Autenticacion.Roles.Consultas.BuscarRoles;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuario;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuarioEstatus;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarUsuario;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.EnviarPassword;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.IniciaSesionUsuarios;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.RegistroUsuario;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPassword;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPasswordToken;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorId;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorNombreUsuario;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPotToken;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.PaginacionUsuario;
using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Aplicacion.Modelo.Imagen;
using Comercio.Aplicacion.Servicios.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [ApiController]
    [Route("Api/V1/[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private IMediator _mediador;
        private IGestorImagenServicio _gestorImagenServicio;
        public UsuarioController(IMediator mediador, IGestorImagenServicio gestorImagenServicio)
        {
            _mediador = mediador;
            _gestorImagenServicio = gestorImagenServicio;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionRespuesta>> Login([FromBody] IniciaSesionUsuariosComando autenticacion)
        {

            return await _mediador.Send(autenticacion);
        }
        [AllowAnonymous]
        [HttpPost("Registrar", Name = "Registrar")]
        public async Task<ActionResult<AutenticacionRespuesta>> Registrar([FromForm] RegistroUsuarioComando solicitud)
        {
            if (solicitud.Foto is not null)
            {
                var resultadoImagen = await _gestorImagenServicio.SubirImagen(new DatosImagenes
                {
                    Imagen = solicitud.Foto!.OpenReadStream(),
                    Nombre = solicitud.Foto.Name
                });
                solicitud.FotoId = resultadoImagen.CodigoPublco;
                solicitud.FotoUrl = resultadoImagen.Url;
            }
            return await _mediador.Send(solicitud);
        }
        [AllowAnonymous]
        [HttpPost("ContrasenaOlvidada", Name = "ContrasenaOlvidada")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ContrasenaOlvidada([FromBody] EnviarPasswordComando solicitud)
        {
            return await _mediador.Send(solicitud);
        }
        [AllowAnonymous]
        [HttpPost("ReiniciarContrasena", Name = "ReiniciarContrasena")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ReiniciarContrasena([FromBody] ReiniciarPasswordTokenComando solicitud)
        {
            return await _mediador.Send(solicitud);
        }
       
        [HttpPost("ActualizarContrasena", Name = "ActualizarContrasena")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> ActualizarContrasena([FromBody] ReiniciarPasswordComando solicitud)
        {
            return await _mediador.Send(solicitud);
        }
        [HttpPut("Actualizar", Name = "Actualizar")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionRespuesta>> Actualizar([FromForm] ActualizarUsuarioComando solicitud)
        {
            if (solicitud.Foto is not null)
            {
                var resultadoImagen = await _gestorImagenServicio.SubirImagen(new DatosImagenes
                {
                    Imagen = solicitud.Foto!.OpenReadStream(),
                    Nombre = solicitud.Foto.Name
                });
                solicitud.FotoId = resultadoImagen.CodigoPublco;
                solicitud.FotoUrl = resultadoImagen.Url;
            }
            return await _mediador.Send(solicitud);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("ActualizarAdministradorUsuario",Name = "ActualizarAdministradorUsuario")]
        [ProducesResponseType(typeof(Usuario),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Usuario>> ActualizarAdministradorUsuario([FromBody] ActualizarAdminUsuarioComando solicitud)
        {
            return await _mediador.Send(solicitud);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("ActualizarAdministradorEstadoUsuario", Name = "ActualizarAdministradorEstadoUsuario")]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Usuario>> ActualizarAdministradorEstadoUsuario([FromBody] ActualizarAdminUsuarioEstatusComando solicitud)
        {
            return await _mediador.Send(solicitud);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet("{id}", Name = "BuscarUsuarioPorId")]
        [ProducesResponseType(typeof(AutenticacionRespuesta), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionRespuesta>> BuscarUsuarioPorId(string id)
        {
            var consulta = new BuscarUsuarioPorIdConsulta(id);
            return await _mediador.Send(consulta);
        }
        [HttpGet("", Name = "UsuarioSesion")]
        [ProducesResponseType(typeof(AutenticacionRespuesta), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionRespuesta>> UsuarioSesion()
        {
            var consulta = new BuscarUsuarioPotTokenConsulta();
            return await _mediador.Send(consulta);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet("NombreUsuario/{nombreUsuario}", Name = "BuscarUsuarioPorNombreUsuario")]
        [ProducesResponseType(typeof(AutenticacionRespuesta), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AutenticacionRespuesta>> BuscarUsuarioPorNombreUsuario(string nombreUsuario)
        {
            var consulta = new BuscarUsuarioPorNombreUsuarioConsulta(nombreUsuario);
            return await _mediador.Send(consulta);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet("paginacionAdmin", Name = "PaginacionUsuario")]
        [ProducesResponseType(typeof(PaginacionVm<Usuario>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<Usuario>>> PaginacionUsuarioAdmin([FromQuery] PaginacionUsuarioConsulta usuario)
        {
           
            var paginacion = await _mediador.Send(usuario);
            return Ok(paginacion);
        }
        [AllowAnonymous]
        [HttpGet("roles", Name = "BuscarListadoRoles")]
        [ProducesResponseType(typeof(List<string>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<List<string>>> BuscarListadoRoles()
        {
            var consulta = new BuscarRolesConsulta();
            var Roles = await _mediador.Send(consulta);
            return Ok(Roles);
        }

    }
}
