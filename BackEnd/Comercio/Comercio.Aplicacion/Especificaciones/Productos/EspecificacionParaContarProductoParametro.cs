using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Dominio.Modelos;


namespace Comercio.Aplicacion.Especificaciones.Productos
{
    public class EspecificacionParaContarProductoParametro : EspecificacionBase<Producto>
    {
        public EspecificacionParaContarProductoParametro(EspecificacionProductoParametro producto)
            : base(
                  b => (string.IsNullOrEmpty(producto.Busqueda) || b.Nombre!.Contains(producto.Busqueda)
                   || b.Descripcion!.Contains(producto.Busqueda)) &&
                   (!producto.IdCategoria.HasValue || b.Idcategoria == producto.IdCategoria) &&
                   (!producto.PrecioMinimo.HasValue || b.Precio >= producto.PrecioMinimo) &&
                   (!producto.PrecioMaximo.HasValue || b.Precio <= producto.PrecioMaximo) &&
                   (!producto.Estatus.HasValue || b.Estatus == producto.Estatus) &&
                   (!producto.Clasificacion.HasValue || b.Clasificacion == producto.Clasificacion)
                  )
        {
                
        }
    }
}
