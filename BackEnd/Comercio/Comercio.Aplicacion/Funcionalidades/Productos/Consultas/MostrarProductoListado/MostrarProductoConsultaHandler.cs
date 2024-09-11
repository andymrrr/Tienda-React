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

namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.MostrarProductoListado
{
    public class MostrarProductoConsultaHandler : IRequestHandler<MostrarProductoConsulta, IReadOnlyList<ProductoVm>>
    {
        private readonly IComercioUoW _comercioUoW;
        private readonly IMapper _mapeo;
        public MostrarProductoConsultaHandler(IComercioUoW comercioUoW, IMapper mapeo)
        {
            _comercioUoW = comercioUoW;
            _mapeo = mapeo;
        }
        public async Task<IReadOnlyList<ProductoVm>> Handle(MostrarProductoConsulta request, CancellationToken cancellationToken)
        {
            var incluir = new List<Expression<Func<Producto, object>>>();
            incluir.Add(I=> I.Imagenes!);
            incluir.Add(C => C.Comentarios!);

           var producto = await _comercioUoW.Repositorio<Producto>().BuscarAsincrono(null, o => o.OrderBy(y => y.FechaCreacion),incluir, true );

            var productovm = _mapeo.Map<IReadOnlyList<ProductoVm>>(producto);
            return productovm;
        }
    }
}
