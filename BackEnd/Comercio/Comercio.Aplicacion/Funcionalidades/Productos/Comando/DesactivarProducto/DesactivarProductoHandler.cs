using AutoMapper;
using Comercio.Aplicacion.Excepciones;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.DesactivarProducto
{
    public class DesactivarProductoHandler : IRequestHandler<DesactivarProductoComando, ProductoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public DesactivarProductoHandler(IMapper mapper, IComercioUoW comercio)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<ProductoVm> Handle(DesactivarProductoComando request, CancellationToken cancellationToken)
        {
            var producto = await _comercio.Repositorio<Producto>().BuscarPorIdAsincrono(request.ProductoId);
            if(producto is null)
            {
                throw new NotFoundException(nameof(producto), request.ProductoId);
            }
            producto.Estatus = producto.Estatus == EstatusProducto.Inactivo ? EstatusProducto.Activo : EstatusProducto.Inactivo;

            await _comercio.Repositorio<Producto>().ActualizarAsincrono(producto);
            var productovm = _mapper.Map<ProductoVm>(producto);


            return productovm;
        }
    }
}
