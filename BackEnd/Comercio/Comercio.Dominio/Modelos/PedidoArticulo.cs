using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class PedidoArticulo: ModeloBase
    {
        public Producto? Producto { get; set; }

        public int IdProducto { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }
       
        public int IdPedido { get; set; }

        public Pedido? Pedido { get; set; }

        public int IdProductoArticulo { get; set; }

        public string? NombreProducto { get; set; }

        public string? ImagenUrl { get; set; }
    }
}
