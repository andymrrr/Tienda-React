using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using MediatR;


namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Consulta.BuscarcarritoCompraId
{
    public class BuscarcarritoCompraIdConsulta : IRequest<CarritoCompraVm>
    {
        public Guid? IdCarritoCompra { get; set; }
        public BuscarcarritoCompraIdConsulta(Guid? idCarritoCompra)
        {
            IdCarritoCompra = idCarritoCompra ?? throw new ArgumentNullException(nameof(idCarritoCompra));            
        }
    }
}
