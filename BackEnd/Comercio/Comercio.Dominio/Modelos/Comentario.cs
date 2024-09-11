using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class Comentario : ModeloBase
    {
        [Column(TypeName ="NVARCHAR(100)")]
        public string? Nombre { get; set; }

        public int Clasificacion { get; set; }

        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Detatalle { get; set; }

        public int IdProducto { get; set; }

        public Producto? Producto { get; set; }

    }
}
