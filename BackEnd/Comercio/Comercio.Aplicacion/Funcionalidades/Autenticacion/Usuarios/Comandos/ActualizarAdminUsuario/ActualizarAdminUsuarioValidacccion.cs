using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuario
{
    public class ActualizarAdminUsuarioValidacccion : AbstractValidator<ActualizarAdminUsuarioComando>
    {
        public ActualizarAdminUsuarioValidacccion()
        {
            RuleFor(N => N.Nombre)
                .NotEmpty().WithMessage("El Nombre no puede estar vacio");

            RuleFor(A => A.Apellido)
                .NotEmpty().WithMessage("El Apellido no puede estar vacio");


            RuleFor(T => T.Telefono)
                .NotEmpty().WithMessage("El Telefono no puede estar vacio");
        }
    }
}
