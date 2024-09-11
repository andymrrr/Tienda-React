using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Pagos.Comando.CrearPago
{
    public class CrearPagoComando : IRequest<PedidoVm>
    {
        public int IdPedido { get; set; }
        public Guid? IdCarritoCompraMaestro { get; set; }
    }
}
