using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class CarritoCompraArticulo: ModeloBase
    {

        public string? NombreProducto { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public string? Imagen { get; set; }

        public string? Categoria { get; set; }

        public Guid? IdCarritoCompraMaestro { get; set; }

        public int IdCarritoCompra { get; set; }

        public int IdProducto { get; set; }

        public int Existencias { get; set; }
        public CarritoCompra? CarritoCompra { get; set; }

        public Producto? Producto { get; set; }


    }
}
