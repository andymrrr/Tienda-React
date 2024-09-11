using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.EliminarArticuloCarritoCompra
{
    public class EliminarArticuloCarritoCompraHandler : IRequestHandler<EliminarArticuloCarritoCompraComando, CarritoCompraVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public EliminarArticuloCarritoCompraHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<CarritoCompraVm> Handle(EliminarArticuloCarritoCompraComando request, CancellationToken cancellationToken)
        {

            var CarritoCompraArticulo = await _comercio.Repositorio<CarritoCompraArticulo>().BuscarEntidadAsincrono(
                x => x.Id == request.Id
            );

            await _comercio.Repositorio<CarritoCompraArticulo>().EliminarAsincrono(CarritoCompraArticulo);


            var incluir = new List<Expression<Func<CarritoCompra, object>>>();
            incluir.Add(p => p.CarritoCompraArticulos!.OrderBy(x => x.NombreProducto));

            var carritoCompra = await _comercio.Repositorio<CarritoCompra>().BuscarEntidadAsincrono(
                x => x.IdCarritoCompraMaestro == CarritoCompraArticulo.IdCarritoCompraMaestro,
                incluir,
                true
            );
            var carritoCompraVm = _mapper.Map<CarritoCompraVm>(carritoCompra);
            return carritoCompraVm;
        }
    }
}
