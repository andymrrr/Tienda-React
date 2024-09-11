using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Comando.CrearDireccion;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.ActualizarOrdenCompra;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.CrearOrdenCompra;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.BuscarOrdenCompraId;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.PaginacionOrdenCompra;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.PaginacionProductos;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private IMediator _mediador;
        private IIdentidadServicio _identidadServicio;
        public OrdenController(IMediator mediador,IIdentidadServicio identidadServicio)
        {
            _mediador = mediador;
            _identidadServicio = identidadServicio;
        }
       
        [HttpPost("Direccion", Name = "CrearDireccion")]
        [ProducesResponseType( (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<DireccionVm>> CrearDireccion([FromBody]CrearDireccionComando solicitud)
        {
            
            var Direccion = await _mediador.Send(solicitud);
            return Ok(Direccion);
        }
        [HttpPost("CrearOrden", Name = "CrearOrden")]
        [ProducesResponseType((int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PedidoVm>> CrearOrden([FromBody] CrearOrdenCompraComando solicitud)
        {

            var Orden = await _mediador.Send(solicitud);
            return Ok(Orden);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("ActualizarOrden", Name = "ActualizarOrden")]
        [ProducesResponseType((int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PedidoVm>> ActualizarOrden([FromBody] ActualizarOrdenCompraComando solicitud)
        {

            var Orden = await _mediador.Send(solicitud);
            return Ok(Orden);
        }
        [HttpGet("BuscarOrdenPorId/{id}", Name = "BuscarOrdenPorId")]
        [ProducesResponseType((int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PedidoVm>> BuscarOrdenPorId(int id)
        {
            var consulta = new BuscarOrdenCompraIdConsulta(id);
            var Orden = await _mediador.Send(consulta);
            return Ok(Orden);
        }
        [HttpGet("paginacionPorUsuario", Name = "paginacionPorUsuario")]
        [ProducesResponseType(typeof(PaginacionVm<PedidoVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<PedidoVm>>> paginacionPorUsuario([FromQuery] PaginacionOrdenCompraConsulta paginacionUsuario)
        {
            paginacionUsuario.Usuario = _identidadServicio.ObtenerUsuarioSesion();

            var paginacion = await _mediador.Send(paginacionUsuario);
            return Ok(paginacion);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet("paginacionAdmin", Name = "paginacionAdmin")]
        [ProducesResponseType(typeof(PaginacionVm<PedidoVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<PedidoVm>>> paginacionAdmin([FromQuery] PaginacionOrdenCompraConsulta paginacionUsuario)
        {
            var paginacion = await _mediador.Send(paginacionUsuario);
            return Ok(paginacion);
        }

    }
}
