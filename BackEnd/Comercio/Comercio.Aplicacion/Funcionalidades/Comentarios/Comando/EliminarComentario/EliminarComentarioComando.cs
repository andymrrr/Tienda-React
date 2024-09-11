using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.EliminarComentario
{
    public class EliminarComentarioComando : IRequest
    {
        public int IdComentario { get; set; }
        public EliminarComentarioComando( int idComentario)
        {
            IdComentario = idComentario == 0 ? throw new ArgumentNullException(nameof(idComentario)) : idComentario;
        }
    }
}
