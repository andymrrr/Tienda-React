using AutoMapper;
using Comercio.Aplicacion.Especificaciones.Comentarios;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Consultas.PaginacionComentario
{
    public class PaginacionComentarioHandler : IRequestHandler<PaginacionComentarioConsulta, PaginacionVm<ComentarioVm>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public PaginacionComentarioHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<PaginacionVm<ComentarioVm>> Handle(PaginacionComentarioConsulta request, CancellationToken cancellationToken)
        {
            var comentarioParametro = new EspecificacionComentarioParametro
            {
                IndicePagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                IdProducto = request.IdProducto
            };
            var especificaciones = new EspecificacionComentario(comentarioParametro);

            var comentarios = await _comercio.Repositorio<Comentario>().BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new EspecificacionParaContarComentarioParametro(comentarioParametro);
            var totalComentario = await _comercio.Repositorio<Comentario>().CantidadAsincrona(cantidadEspecificaciones);
            var formulaRedondeo = Convert.ToDecimal(totalComentario) / Convert.ToDecimal(request.TamanoPagina);
            var redondeado = Math.Ceiling(formulaRedondeo);
            var totalPagina = Convert.ToInt32(redondeado);

            var comentariovm = _mapper.Map<IReadOnlyList<ComentarioVm>>(comentarios);

            var productoPorPagina = comentarios.Count();

            var paginacion = new PaginacionVm<ComentarioVm>
            {
                Cantidad = totalComentario,
                Datos = comentariovm,
                CantidadPagina = totalPagina,
                Indicepagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                ResultadoPorPagina = productoPorPagina

            };
            return paginacion;
        }
    }
}
