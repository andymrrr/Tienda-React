using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class Pedido : ModeloBase
    {
        public Pedido()
        {

        }
        public Pedido(string nombreComprador, string? emailComprador,
                      PedidoDireccion pedidoDireccion, decimal subTotal,
                      decimal total, decimal impuesto, decimal precioEnvio)
        {
            NombreComprador = nombreComprador;
            UsuarioComprador = emailComprador;
            PedidoDireccion = pedidoDireccion;
            SubTotal = subTotal;
            Total = total;
            Impuesto = impuesto;
            PrecioEnvio = precioEnvio;
        }
        public string? NombreComprador { get; set; }

        public string? UsuarioComprador { get; set; }

        public PedidoDireccion? PedidoDireccion { get; set; }

        public IReadOnlyList<PedidoArticulo>? PedidoArticulos { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        public EstatusPedidos Estatus { get; set; } = EstatusPedidos.Pendiente;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Impuesto { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioEnvio { get; set; }

        public string? IdIntencionPago { get; set; }

        public string? SecretoCliente { get; set; }

        public string? StripeApiKey { get; set; }
    }
}
