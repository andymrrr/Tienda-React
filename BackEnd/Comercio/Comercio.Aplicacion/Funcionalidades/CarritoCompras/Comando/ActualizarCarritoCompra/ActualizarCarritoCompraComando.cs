using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.ActualizarCarritoCompra
{
    public class ActualizarCarritoCompraComando : IRequest<CarritoCompraVm>
    {
        public Guid? IdCarritoCompra { get; set; }

        public List<CarrritoCompraArticulosVm>? CarritoCompraArticulos { get; set; }
    }
}
