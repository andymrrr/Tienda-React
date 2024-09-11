using Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.BuscarCategorias;
using Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.Vm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Comercio.Api.Controllers
{
    [Route("Api/V1/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private IMediator _mediador;
        public CategoriaController(IMediator mediador)
        {
            _mediador = mediador;
        }
        [AllowAnonymous]
        [HttpGet("BuscarListadoCategoria", Name = "BuscarListadoCategoria")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoriaVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<IReadOnlyList<CategoriaVm>>> BuscarListadoCategoria()
        {
            var consulta = new BuscarCategoriasConsulta();
            var Paises = await _mediador.Send(consulta);
            return Ok(Paises);
        }
    }
}
