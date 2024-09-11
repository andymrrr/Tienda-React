

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms
{
    public class CarrritoCompraArticulosVm
    {
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public string? Imagen { get; set; }
        public string? Categoria { get; set; }

        public int Existencias { get; set; }

        public decimal TotalLineas {
            get
            {
                return Math.Round(Cantidad * Precio, 2);
            }

            set { }
        
        }
    }
}
