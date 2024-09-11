using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.MostrarProductoListado;
using Microsoft.AspNetCore.Authorization;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.PaginacionProductos;
using Comercio.Dominio.Modelos;
using Comercio.Aplicacion.Funcionalidades.Productos.Consultas.BuscarProductoPorId;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.RegistrarProducto;
using Comercio.Aplicacion.Modelo.Autorizacion;
using Comercio.Aplicacion.Modelo.Imagen;
using Comercio.Aplicacion.Servicios.Interfaz;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.DesactivarProducto;

namespace Comercio.Api.Controllers
{
    [ApiController]
    [Route("Api/V1/[Controller]")]
    public class ProductoController : ControllerBase
    {
        private IMediator _mediador;
        private IGestorImagenServicio _gestorImagenServicio;
        public ProductoController(IMediator mediador, IGestorImagenServicio gestorImagenServicio)
        {
            _mediador = mediador;
            _gestorImagenServicio = gestorImagenServicio;
        }
        [AllowAnonymous]
        [HttpGet("listar", Name = "MostrarProducto")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductoVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductoVm>>> MostrarProducto()
        {
            var consulta = new MostrarProductoConsulta();
            var producto = await _mediador.Send(consulta);
            return Ok(producto);
        }
        [AllowAnonymous]
        [HttpGet("paginacion", Name = "PaginacionProducto")]
        [ProducesResponseType(typeof(PaginacionVm<ProductoVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<ProductoVm>>> PaginacionProducto([FromQuery] PaginacionProductoConsulta producto)
        {
            producto.Estatus = EstatusProducto.Activo;
            var paginacion = await _mediador.Send(producto);
            return Ok(paginacion);
        }
        [Authorize(Roles =AppRoles.Admin)]
        [HttpGet("PaginacionProductoAdmin", Name = "PaginacionProductoAdmin")]
        [ProducesResponseType(typeof(PaginacionVm<ProductoVm>), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<PaginacionVm<ProductoVm>>> PaginacionProductoAdmin([FromQuery] PaginacionProductoConsulta producto)
        {
            
            var paginacion = await _mediador.Send(producto);
            return Ok(paginacion);
        }
        [AllowAnonymous]
        [HttpGet("{id}", Name = "BuscarProductoPorId")]
        [ProducesResponseType(typeof(ProductoVm),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductoVm>> BuscarProductoPorId (int id)
        {
            var connsulta = new BuscarProductoPorIdConsulta(id);
            var producto =  await _mediador.Send(connsulta);
            return Ok(producto);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost("RegistrarProducto", Name = "RegistrarProducto")]
        [ProducesResponseType(typeof(ProductoVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductoVm>> RegistrarProducto([FromForm] RegistrarProductoComando registrar)
        {
            var listadoFoto = new List<RegistrarProductoImagenesComando>();
            if(registrar.Fotos is not null)
            {
                foreach (var foto in registrar.Fotos!)
                {
                    var resultadoImagen = await _gestorImagenServicio.SubirImagen(new DatosImagenes
                    {
                        Imagen = foto.OpenReadStream(),
                        Nombre = foto.Name
                    });
                    var fotoComando = new RegistrarProductoImagenesComando
                    {
                        CodigoPublco = resultadoImagen.CodigoPublco,
                        Url = resultadoImagen.Url,
                    };
                    listadoFoto.Add(fotoComando);
                }
                registrar.Imagenes = listadoFoto;
            }
               
            var producto = await _mediador.Send(registrar);
            return Ok(producto);
        }

        //[Authorize(Roles = AppRoles.Admin)]
        //[HttpPut("ActualizarProducto", Name = "ActualizarProducto")]
        //[ProducesResponseType(typeof(ProductoVm), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<ProductoVm>> ActualizarProducto([FromForm] ActualizarProductoComando registrar)
        //{
        //    var listadoFoto = new List<RegistrarProductoImagenesComando>();
        //    if (registrar.Fotos is not null)
        //    {
        //        foreach (var foto in registrar.Fotos!)
        //        {
        //            var resultadoImagen = await _gestorImagenServicio.SubirImagen(new DatosImagenes
        //            {
        //                Imagen = foto.OpenReadStream(),
        //                Nombre = foto.Name
        //            });
        //            var fotoComando = new RegistrarProductoImagenesComando
        //            {
        //                CodigoPublco = resultadoImagen.CodigoPublco,
        //                Url = resultadoImagen.Url,
        //            };
        //            listadoFoto.Add(fotoComando);
        //        }
        //        registrar.Imagenes = listadoFoto;
        //    }

        //    var producto = await _mediador.Send(registrar);
        //    return Ok(producto);
        //}
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut("DesctivarProducto/{id}", Name = "DesctivarProducto")]
        [ProducesResponseType(typeof(ProductoVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductoVm>> DesctivarProducto(int id)
        {
            var consulta = new DesactivarProductoComando(id);
            var producto = await _mediador.Send(consulta);
            return Ok(producto);
        }
    }
}
