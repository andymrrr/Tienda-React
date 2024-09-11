using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.EnviarPassword
{
    public class EnviarPasswordComando : IRequest<string>
    {
        public string? Email { get; set; }
    }
}
