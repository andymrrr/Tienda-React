using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.BuscarProductoPorId
{
    internal class BuscarProductoPorIdConsultaHandler : IRequestHandler<BuscarProductoPorIdConsulta, ProductoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapeo;
        public BuscarProductoPorIdConsultaHandler(IMapper mapeo, IComercioUoW comercio)
        {
            _comercio = comercio;
            _mapeo = mapeo; 
        }
        public async Task<ProductoVm> Handle(BuscarProductoPorIdConsulta request, CancellationToken cancellationToken)
        {

            var incluir = new List<Expression<Func<Producto, object>>>();
            incluir.Add(C => C.Categoria!);
            incluir.Add(i => i.Imagenes!);
            incluir.Add(c => c.Comentarios!.OrderByDescending(f=> f.FechaCreacion));

            var producto = await _comercio.Repositorio<Producto>().BuscarEntidadAsincrono(
                    x=> x.Id == request.IdProducto,
                    incluir,
                    true
                );
            var productoVm = _mapeo.Map<ProductoVm>(producto);

            return productoVm;

        }
    }
}
