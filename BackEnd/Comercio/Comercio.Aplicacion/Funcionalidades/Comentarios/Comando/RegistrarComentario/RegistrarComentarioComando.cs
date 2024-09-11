using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.RegistrarComentario
{
    public class RegistrarComentarioComando : IRequest<ComentarioVm>
    {
        public string? Nombre { get; set; }

        public int Clasificacion { get; set; }

        public string? Detatalle { get; set; }

        public int IdProducto { get; set; }
    }
}
