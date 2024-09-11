using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.BuscarUsuarioPorNombreUsuario
{
    public class BuscarUsuarioPorNombreUsuarioConsulta :IRequest<AutenticacionRespuesta>
    {
        public string? NombreUsuario { get; set; }
        public BuscarUsuarioPorNombreUsuarioConsulta(string nombreUsuario)
        {
            NombreUsuario = nombreUsuario ?? throw new ArgumentNullException(nameof(nombreUsuario));
        }
    }
}
