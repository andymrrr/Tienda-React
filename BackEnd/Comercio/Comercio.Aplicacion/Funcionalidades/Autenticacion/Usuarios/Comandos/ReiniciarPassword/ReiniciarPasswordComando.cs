using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPassword
{
    public class ReiniciarPasswordComando : IRequest
    {
        public string? AntiguoPassword { get; set; }
        public string? NuevoPassword { get; set; }

    }
}
