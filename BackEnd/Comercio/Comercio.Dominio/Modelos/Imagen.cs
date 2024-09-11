using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class Imagen : ModeloBase
    {
        [Column(TypeName ="NVARCHAR(4000)")]
        public string? Url { get; set; }

        public int IdProducto { get; set; }

        public string? CodigoPublco { get; set; }

        public Producto? Producto { get; set; }
    }
}
