using AutoMapper;
using Comercio.Aplicacion.Excepciones;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.EliminarComentario
{
    public class EliminarComentarioHandler : IRequestHandler<EliminarComentarioComando>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public EliminarComentarioHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper; 
        }
        public async Task<Unit> Handle(EliminarComentarioComando request, CancellationToken cancellationToken)
        {
            var comentario = await _comercio.Repositorio<Comentario>().BuscarPorIdAsincrono(request.IdComentario);
            if (comentario is null)
            {
                throw new NotFoundException(nameof(comentario), request.IdComentario);
            }
            _comercio.Repositorio<Comentario>().EliminarEntidad(comentario);
            await _comercio.Completo();

            return Unit.Value;
            
        }
    }
}
