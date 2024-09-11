using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdConsulta : IRequest<AutenticacionRespuesta>
    {
        public string? UsuarioId { get; set; }
        public BuscarUsuarioPorIdConsulta( string usuarioid)
        {
            UsuarioId = usuarioid ?? throw new ArgumentNullException(nameof(UsuarioId));
        }
    }
}
