using Comercio.Aplicacion.Funcionalidades.Paises.Consulta.BuscarListadoPaises;
using Comercio.Aplicacion.Funcionalidades.Paises.Vm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private IMediator _mediador;
        public PaisController(IMediator mediador)
        {
            _mediador = mediador;
        }
        [AllowAnonymous]
        [HttpGet("BuscarListadoPaises", Name = "BuscarListadoPaises")]
        [ProducesResponseType(typeof(IReadOnlyList<PaisesVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<IReadOnlyList<PaisesVm>>> BuscarListadoPaises()
        {
            var consulta = new BuscarListadoPaisesConsulta();
            var Paises = await _mediador.Send(consulta);
            return Ok(Paises);
        }
    }
}
