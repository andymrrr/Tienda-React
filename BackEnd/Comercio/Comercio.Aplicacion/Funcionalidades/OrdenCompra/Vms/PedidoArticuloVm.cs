
namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms
{
    public class PedidoArticuloVm
    {
        public int IdProducto { get; set; }
       
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public int IdPedido { get; set; }

        public int IdProductoArticulo { get; set; }

        public string? NombreProducto { get; set; }

        public string? ImagenUrl { get; set; }
    }
}
