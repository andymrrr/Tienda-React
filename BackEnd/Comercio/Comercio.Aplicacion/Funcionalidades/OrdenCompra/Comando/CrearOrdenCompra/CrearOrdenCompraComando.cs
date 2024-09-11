using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using MediatR;


namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.CrearOrdenCompra
{
    public class CrearOrdenCompraComando : IRequest<PedidoVm>
    {
        public Guid IdCarritoCompra { get; set; } 
    }
}
