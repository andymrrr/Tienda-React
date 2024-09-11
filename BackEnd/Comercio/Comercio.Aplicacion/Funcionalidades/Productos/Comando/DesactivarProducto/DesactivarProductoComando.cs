using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.DesactivarProducto
{
    public class DesactivarProductoComando : IRequest<ProductoVm>
    {
        public int ProductoId { get; set; }

        public DesactivarProductoComando(int productoId)
        {
            ProductoId = productoId == 0 ? throw new ArgumentException(nameof(productoId)) : productoId;
        }
    }
}
