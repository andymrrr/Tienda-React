using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Modelo.Pedidos;
using Comercio.Dominio.Modelos;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms
{
    public class PedidoVm 
    {
        public int Id { get; set; }

        public DireccionVm? PedidoDireccion { get; set; }

        public List<PedidoArticuloVm>? PedidoArticulos { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }

        public decimal PrecioEnvio { get; set; }

        public EstatusPedidos Estatus { get; set; }

        public string? IdIntencionPago { get; set; }

        public string? SecretoCliente { get; set; }

        public string? StripeApiKey { get; set; }

        public string? NombreComprador { get; set; }

        public string? UsuarioComprador { get; set; }

        public int Cantidad {
            get {
                return PedidoArticulos!.Sum(s => s.Cantidad);
            }
            set { }
        }

        public string? NombreEstatus 
        {
            get {
                switch (Estatus)
                {
                    case EstatusPedidos.Pendiente:
                        return EstatusPedidosTexto.Pendiente;
                    case EstatusPedidos.Completada:
                        return EstatusPedidosTexto.Completada;
                    case EstatusPedidos.Enviado:
                        return EstatusPedidosTexto.Enviado;
                    case EstatusPedidos.Error:
                        return EstatusPedidosTexto.Error;
                    default:
                        return EstatusPedidosTexto.Error;
                }
            }
            set { }
        }
    }
}
