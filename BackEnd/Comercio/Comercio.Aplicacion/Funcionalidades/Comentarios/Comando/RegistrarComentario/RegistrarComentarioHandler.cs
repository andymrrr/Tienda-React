using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.RegistrarComentario
{
    public class RegistrarComentarioHandler : IRequestHandler<RegistrarComentarioComando, ComentarioVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public RegistrarComentarioHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }

        public async Task<ComentarioVm> Handle(RegistrarComentarioComando request, CancellationToken cancellationToken)
        {
            var comentario = new Comentario
            {
                Detatalle = request.Detatalle,
                Nombre = request.Nombre,
                IdProducto = request.IdProducto,
                Clasificacion = request.Clasificacion

            };
            _comercio.Repositorio<Comentario>().AgregarEntidad(comentario);
            var resultado = await _comercio.Completo();
            if (resultado <= 0 )
            {
                throw new Exception("No se pudo guardar el comentario");

            }
            var comentarioVm = _mapper.Map<ComentarioVm>(comentario);

            return comentarioVm;
        }
    }
}
