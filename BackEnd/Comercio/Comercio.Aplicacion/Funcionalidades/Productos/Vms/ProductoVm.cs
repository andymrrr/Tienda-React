using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Imagenes.Vms;
using Comercio.Aplicacion.Modelo.Producto;
using Comercio.Dominio.Modelos;


namespace Comercio.Aplicacion.Funcionalidades.Productos.Vms
{
    public class ProductoVm
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public decimal? Precio { get; set; }

        public int Clasificacion { get; set; }

        public string? Vendedor { get; set; }
        public string? Descripcion { get; set; }

        public int? Existencias { get; set; }

        public virtual ICollection<ComentarioVm>? Comentarios { get; set; }

        public virtual ICollection<ImagenVm>? Imagenes { get; set; }

        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public int NumeroComentario { get; set; }
        public EstatusProducto Estatus { get; set; }
        public string? EstatusTexto
        {
            get
            {
                switch (Estatus)
                {
                    case EstatusProducto.Activo:
                        return EstatusProductoTexto.Activo;
                    case EstatusProducto.Inactivo:
                       return EstatusProductoTexto.Inactivo;
                    default:
                        return EstatusProductoTexto.Inactivo; 
                }
            }
        }
    }
}
