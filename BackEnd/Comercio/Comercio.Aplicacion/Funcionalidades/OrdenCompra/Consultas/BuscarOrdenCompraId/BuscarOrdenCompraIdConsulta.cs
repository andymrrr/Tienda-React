using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using MediatR;


namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.BuscarOrdenCompraId
{
    public class BuscarOrdenCompraIdConsulta : IRequest<PedidoVm>
    {
        public int IdPedido { get; set; }
        public BuscarOrdenCompraIdConsulta(int idPedido)
        {
            IdPedido = idPedido == 0 ? throw new ArgumentNullException(nameof(idPedido)) : idPedido;
        }
    }
}
