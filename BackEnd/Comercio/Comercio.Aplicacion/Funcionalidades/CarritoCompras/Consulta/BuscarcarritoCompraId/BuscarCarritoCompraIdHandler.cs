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

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Consulta.BuscarcarritoCompraId
{
    public class BuscarcarritoCompraIdHandler : IRequestHandler<BuscarcarritoCompraIdConsulta, CarritoCompraVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public BuscarcarritoCompraIdHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<CarritoCompraVm> Handle(BuscarcarritoCompraIdConsulta request, CancellationToken cancellationToken)
        {
            var incluir = new List<Expression<Func<CarritoCompra, object>>>();

            incluir.Add(C => C.CarritoCompraArticulos!.OrderBy(o => o.NombreProducto));

            var carritoCompra = await _comercio.Repositorio<CarritoCompra>().BuscarEntidadAsincrono(
                    x=> x.IdCarritoCompraMaestro == request.IdCarritoCompra,
                    incluir,
                    true
                );
            if ( carritoCompra is null)
            {
                carritoCompra = new CarritoCompra
                {
                    IdCarritoCompraMaestro = request.IdCarritoCompra,
                    CarritoCompraArticulos = new List<CarritoCompraArticulo>()
                };
                _comercio.Repositorio<CarritoCompra>().AgregarEntidad(carritoCompra);
                await _comercio.Completo();
            }

            
            var CarritoCompraVm = _mapper.Map<CarritoCompraVm>(carritoCompra);

            return CarritoCompraVm;

        }
    }
}
