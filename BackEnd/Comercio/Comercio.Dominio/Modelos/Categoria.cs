using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class Categoria: ModeloBase
    {
        [Column(TypeName ="NVARCHAR(100)")]
        public string? Nombre { get; set; }
        public virtual ICollection<Producto>? Productos { get; set; }
    }
}
