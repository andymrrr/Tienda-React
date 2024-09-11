using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones.Comentarios
{
    public class EspecificacionComentario : EspecificacionBase<Comentario>
    {
        public EspecificacionComentario(EspecificacionComentarioParametro comentario)
           : base(
                 C => (!comentario.IdProducto.HasValue || C.IdProducto == comentario.IdProducto)
                 )
        {
            var skip = comentario.TamanoPagina * (comentario.IndicePagina - 1);
            AplicarPginacion(skip, comentario.TamanoPagina);

            if (!string.IsNullOrEmpty(comentario.Ordenar))
            {
                switch (comentario.Ordenar)
                {
                    case "fechacreacionAsc":
                        AgregarOrdenarPor(p => p.FechaCreacion!);
                        break;
                    case "fechacreacionDesc":
                        AgregarOrdenarDescendiente(p => p.FechaCreacion!);
                        break;
                    
                    
                    default:
                        AgregarOrdenarPor(p => p.FechaCreacion!);
                        break;

                }
            }
            else
            {
                AgregarOrdenarDescendiente(f => f.FechaCreacion!);
            }



        }


    }
}
