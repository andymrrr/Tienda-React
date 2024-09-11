using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.IniciaSesionUsuarios
{
    public class IniciaSesionUsuariosComandoValidaccion : AbstractValidator<IniciaSesionUsuariosComando>
    {
        public IniciaSesionUsuariosComandoValidaccion()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El Correo electronico no puede estar vacio");
        }
    }
}
