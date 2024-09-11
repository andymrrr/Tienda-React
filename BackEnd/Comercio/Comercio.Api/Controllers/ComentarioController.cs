using Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.EliminarComentario;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.RegistrarComentario;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Consultas.PaginacionComentario;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.DesactivarProducto;
using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.PaginacionProductos;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private IMediator _mediador;
        public ComentarioController(IMediator mediador)
        {
            _mediador = mediador;
        }
       
        [HttpPost("AgregarComentario", Name = "AgregarComentario")]
        [ProducesResponseType(typeof(ComentarioVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ComentarioVm>> AgregarComentario([FromBody] RegistrarComentarioComando registrar)
        {
            var comentario = await _mediador.Send(registrar);
            return Ok(comentario);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete("EliminarComentario/{id}", Name = "EliminarComentario")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> EliminarComentario( int id)
        {
            var comando = new EliminarComentarioComando(id);
            var comentario = await _mediador.Send(comando);
            return Ok(comentario);
        }
        [AllowAnonymous]
        [HttpGet("paginacion", Name = "PaginacionComentario")]
        [ProducesResponseType(typeof(PaginacionVm<ComentarioVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<ComentarioVm>>> PaginacionComentario([FromQuery] PaginacionComentarioConsulta comentario)
        {
           
            var paginacion = await _mediador.Send(comentario);
            return Ok(paginacion);
        }
    }
}
