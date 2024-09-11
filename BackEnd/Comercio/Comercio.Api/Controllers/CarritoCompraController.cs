using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.ActualizarCarritoCompra;
using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.EliminarArticuloCarritoCompra;
using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Consulta.BuscarcarritoCompraId;
using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {
        private IMediator _mediador;
        public CarritoCompraController(IMediator mediador)
        {
            _mediador = mediador;
        }
        [AllowAnonymous]
        [HttpGet("BuscarCarritoCompraPorId/{id}", Name = "BuscarCarritoCompraPorId")]
        [ProducesResponseType(typeof(CarritoCompraVm), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<CarritoCompraVm>> BuscarCarritoCompraPorId(Guid? id)
        {
            var carritoCompraId = id == Guid.Empty ? Guid.NewGuid() : id;
            var consulta = new BuscarcarritoCompraIdConsulta(carritoCompraId);
            var carritoCompra = await _mediador.Send(consulta);
            return Ok(carritoCompra);
        }
        [AllowAnonymous]
        [HttpPut("ActualizarCarritoCompra/{id}", Name = "ActualizarCarritoCompra")]
        [ProducesResponseType(typeof(CarritoCompraVm), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<CarritoCompraVm>> ActualizarCarritoCompra(Guid? id, ActualizarCarritoCompraComando solicitud)
        
        {
            solicitud.IdCarritoCompra = id; 
            var carritoCompra = await _mediador.Send(solicitud);
            return Ok(carritoCompra);
        }
        [AllowAnonymous]
        [HttpDelete("Articulo/{id}", Name = "EliminarArticuloCarritoCompra")]
        [ProducesResponseType(typeof(CarritoCompraVm), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<CarritoCompraVm>> EliminarArticuloCarritoCompra(int id)
        {
            var articuloEliminar = new EliminarArticuloCarritoCompraComando
            {
                Id = id,
            };
            var carritoCompra = await _mediador.Send(articuloEliminar);
            return Ok(carritoCompra);
        }
    }
}
