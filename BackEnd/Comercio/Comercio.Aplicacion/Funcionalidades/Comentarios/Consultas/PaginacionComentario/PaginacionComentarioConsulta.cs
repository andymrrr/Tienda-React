using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Consultas.PaginacionComentario
{
    public class PaginacionComentarioConsulta : ConsultaBasePaginacion, IRequest<PaginacionVm<ComentarioVm>>
    {
        public int? IdProducto { get; set; }

    }
}
