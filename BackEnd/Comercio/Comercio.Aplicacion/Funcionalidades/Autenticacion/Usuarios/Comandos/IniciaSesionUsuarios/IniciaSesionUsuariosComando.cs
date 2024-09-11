using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.IniciaSesionUsuarios
{
    public class IniciaSesionUsuariosComando : IRequest<AutenticacionRespuesta>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
