using AutoMapper;
using Comercio.Aplicacion.Excepciones;
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

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.ActualizarCarritoCompra
{
    public class ActualizarCarritoCompraHandler :
        IRequestHandler<ActualizarCarritoCompraComando, CarritoCompraVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public ActualizarCarritoCompraHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<CarritoCompraVm> Handle(ActualizarCarritoCompraComando request, CancellationToken cancellationToken)
        {
            
            
            var carritoCompra = await _comercio.Repositorio<CarritoCompra>().BuscarEntidadAsincrono(d => d.IdCarritoCompraMaestro == request.IdCarritoCompra);

            if (carritoCompra is null)
            {
                throw new NotFoundException(nameof(carritoCompra), request.IdCarritoCompra!);
            }

            var articulo = await _comercio.Repositorio<CarritoCompraArticulo>().BuscarAsincrono(d=> d.IdCarritoCompraMaestro == request.IdCarritoCompra);

            _comercio.Repositorio<CarritoCompraArticulo>().EliminarRango(articulo);
           
            var articuloAgregar = _mapper.Map<List<CarritoCompraArticulo>>(request.CarritoCompraArticulos);

            articuloAgregar.ForEach(x =>
            {
                x.IdCarritoCompra = carritoCompra.Id;
                x.IdCarritoCompraMaestro = request.IdCarritoCompra;
            });

            _comercio.Repositorio<CarritoCompraArticulo>().AgregarRango(articuloAgregar);

            var resultado = await _comercio.Completo();
            if (resultado <0)
            {
                throw new Exception("No se pudo agregar los articulos al carrito de compra");
            }

            var incluir = new List<Expression<Func<CarritoCompra, object>>>();

            incluir.Add(C => C.CarritoCompraArticulos!.OrderBy(o => o.NombreProducto));

            var carrito = await _comercio.Repositorio<CarritoCompra>().BuscarEntidadAsincrono(
                    x => x.IdCarritoCompraMaestro == request.IdCarritoCompra,
                    incluir,
                    true
                );
            var carritoVm = _mapper.Map<CarritoCompraVm>(carrito);

            return carritoVm;
        }
    }
}
