using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Dominio.Modelos;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.ActualizarOrdenCompra
{
    public class ActualizarOrdenCompraComando : IRequest<PedidoVm>
    {
        public int IdPedido { get; set; }
        public EstatusPedidos  Estatus { get; set; }
    }
}
