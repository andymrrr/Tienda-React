using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.PaginacionOrdenCompra
{
    public class PaginacionOrdenCompraConsulta : ConsultaBasePaginacion, IRequest<PaginacionVm<PedidoVm>>
    {
        public int? Id { get; set; }
        public string? Usuario { get; set; }
    }
}
