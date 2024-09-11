using AutoMapper;
using Comercio.Aplicacion.Especificaciones.OrdenCompra;
using Comercio.Aplicacion.Especificaciones.Productos;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.PaginacionOrdenCompra
{
    internal class PaginacionOrdenCompraHandler : IRequestHandler<PaginacionOrdenCompraConsulta, PaginacionVm<PedidoVm>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public PaginacionOrdenCompraHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }

        public async Task<PaginacionVm<PedidoVm>> Handle(PaginacionOrdenCompraConsulta request, CancellationToken cancellationToken)
        {
            var Parametro = new EspecificacionOrdenCompraParametro
            {
                IndicePagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                Id = request.Id,
                Usuario = request.Usuario,
                
            };
            var especificaciones = new EspecificacionOrdenCompra(Parametro);

            var ordenCompra = await _comercio.Repositorio<Pedido>().BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new EspecificacionParaContarOrdenCompraParametro(Parametro);
            var totalProducto = await _comercio.Repositorio<Pedido>().CantidadAsincrona(cantidadEspecificaciones);
            var formulaRedondeo = Convert.ToDecimal(totalProducto) / Convert.ToDecimal(request.TamanoPagina);
            var redondeado = Math.Ceiling(formulaRedondeo);
            var totalPagina = Convert.ToInt32(redondeado);

            var pedidoVm = _mapper.Map<IReadOnlyList<PedidoVm>>(ordenCompra);

            var productoPorPagina = ordenCompra.Count();

            var paginacion = new PaginacionVm<PedidoVm>
            {
                Cantidad = totalProducto,
                Datos = pedidoVm,
                CantidadPagina = totalPagina,
                Indicepagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                ResultadoPorPagina = productoPorPagina

            };
            return paginacion;
        }
    }
}
