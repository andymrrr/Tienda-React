using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones.Productos
{
    public class EspecificacionProducto : EspecificacionBase<Producto>
    {
        public EspecificacionProducto(EspecificacionProductoParametro producto): base
            (
             b => (string.IsNullOrEmpty(producto.Busqueda) || b.Nombre!.Contains(producto.Busqueda)
                   || b.Descripcion!.Contains(producto.Busqueda)) &&
                   (!producto.IdCategoria.HasValue || b.Idcategoria == producto.IdCategoria) &&
                   (!producto.PrecioMinimo.HasValue || b.Precio >= producto.PrecioMinimo) &&
                   (!producto.PrecioMaximo.HasValue || b.Precio <= producto.PrecioMaximo) &&
                   (!producto.Estatus.HasValue || b.Estatus == producto.Estatus) && 
                   (!producto.Clasificacion.HasValue || b.Clasificacion == producto.Clasificacion)
            )
        {
            AgregarIncluir(p => p.Comentarios!);
            AgregarIncluir(p => p.Imagenes!);
            var skip = producto.TamanoPagina * (producto.IndicePagina - 1);
            AplicarPginacion(skip, producto.TamanoPagina);

            if (!string.IsNullOrEmpty(producto.Ordenar))
            {
                switch (producto.Ordenar)
                {
                    case "nombreAsc":
                        AgregarOrdenarPor(p=> p.Nombre!);
                        break;
                    case "nombreDesc":
                        AgregarOrdenarDescendiente(p => p.Nombre!);
                        break;
                    case "precioAsc":
                        AgregarOrdenarPor(p => p.Precio!);
                        break;
                    case "precioDesc":
                        AgregarOrdenarDescendiente(p => p.Precio!);
                        break;
                    case "comentarioAsc":
                        AgregarOrdenarPor(p => p.Comentarios!);
                        break;
                    case "comentarioDesc":
                        AgregarOrdenarDescendiente(p => p.Comentarios!);
                        break;
                    default:
                        AgregarOrdenarPor(p=> p.FechaCreacion!);
                        break;

                }
            }
            else
            {
                AgregarOrdenarDescendiente(f=> f.FechaCreacion!);
            }
        }
    }
}
