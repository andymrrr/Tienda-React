using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Dominio.Modelos;
using MediatR;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.PaginacionUsuario
{
    public class PaginacionUsuarioConsulta : ConsultaBasePaginacion, IRequest<PaginacionVm<Usuario>>
    {
    }
}
