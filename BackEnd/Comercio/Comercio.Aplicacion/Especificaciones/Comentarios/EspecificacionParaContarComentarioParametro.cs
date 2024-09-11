
using Comercio.Dominio.Modelos;


namespace Comercio.Aplicacion.Especificaciones.Comentarios
{
    public class EspecificacionParaContarComentarioParametro : EspecificacionBase<Comentario>
    {
        public EspecificacionParaContarComentarioParametro(EspecificacionComentarioParametro comentario)
           : base(
                 C => (!comentario.IdProducto.HasValue || C.IdProducto == comentario.IdProducto) 
                  
                
                 )
        {

        }
    }
}
