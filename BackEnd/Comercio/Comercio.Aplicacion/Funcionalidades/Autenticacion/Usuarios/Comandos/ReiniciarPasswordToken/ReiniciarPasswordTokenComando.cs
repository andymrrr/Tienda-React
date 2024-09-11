using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ReiniciarPasswordToken
{
    public class ReiniciarPasswordTokenComando : IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmarPassword { get; set; }
        public string? Token { get; set; }
    }
}
