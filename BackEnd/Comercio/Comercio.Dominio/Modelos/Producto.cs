
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comercio.Dominio.Modelos
{
    public class Producto: ModeloBase
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string? Nombre { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Precio { get; set; }

        [Column(TypeName = "NVARCHAR(4000)")]
        public string? Descripcion { get; set; }

        public int Clasificacion { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string? Vendedor { get; set; }

        public int Existencias { get; set; }

        public EstatusProducto Estatus { get; set; } = EstatusProducto.Activo;

        public int Idcategoria { get; set; }

        public Categoria? Categoria { get; set; }

        public ICollection<Comentario>? Comentarios { get; set; }

        public ICollection<Imagen>? Imagenes { get; set; }


    }
}
