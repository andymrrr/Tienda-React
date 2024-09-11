

using AutoMapper;
using Comercio.Aplicacion.Especificaciones.Productos;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.PaginacionProductos
{
    public class PaginacionProductoHandler : IRequestHandler<PaginacionProductoConsulta, PaginacionVm<ProductoVm>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public PaginacionProductoHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<PaginacionVm<ProductoVm>> Handle(PaginacionProductoConsulta request, CancellationToken cancellationToken)
        {
            var productoParametro = new EspecificacionProductoParametro
            {

                IndicePagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                IdCategoria = request.IdCategoria,
                PrecioMaximo = request.PrecioMaximo,
                PrecioMinimo = request.PrecioMinimo,
                Clasificacion = request.Clasificacion,
                Estatus = request.Estatus
            };
            var especificaciones = new EspecificacionProducto(productoParametro);

            var productos = await _comercio.Repositorio<Producto>().BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new EspecificacionParaContarProductoParametro(productoParametro);
            var totalProducto = await _comercio.Repositorio<Producto>().CantidadAsincrona(cantidadEspecificaciones);
            var formulaRedondeo = Convert.ToDecimal(totalProducto) / Convert.ToDecimal(request.TamanoPagina);
            var redondeado = Math.Ceiling(formulaRedondeo);
            var totalPagina = Convert.ToInt32(redondeado);

            var productovm = _mapper.Map<IReadOnlyList<ProductoVm>>(productos);

            var productoPorPagina = productos.Count();

            var paginacion = new PaginacionVm<ProductoVm> { 
                Cantidad = totalProducto,
                Datos = productovm,
                CantidadPagina = totalPagina,
                Indicepagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                ResultadoPorPagina = productoPorPagina

            };
            return paginacion;


        }
    }
}
