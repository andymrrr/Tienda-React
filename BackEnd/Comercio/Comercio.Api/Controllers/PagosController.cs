
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Funcionalidades.Pagos.Comando.CrearPago;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private IMediator _mediador;
        public PagosController(IMediator mediador)
        {
            _mediador = mediador;
        }
        [HttpGet("RealizarPagos", Name = "RealizarPagos")]
        [ProducesResponseType(typeof(IReadOnlyList<PedidoVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PedidoVm>> RealizarPagos([FromBody] CrearPagoComando solicitud)
        {
            var Pago = await _mediador.Send(solicitud);
            return Ok(Pago);
        }
    }
}
