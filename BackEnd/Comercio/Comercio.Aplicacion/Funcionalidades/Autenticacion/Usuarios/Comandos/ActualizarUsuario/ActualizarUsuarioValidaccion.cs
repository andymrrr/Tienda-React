using FluentValidation;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarUsuario
{
    public class ActualizarUsuarioValidaccion : AbstractValidator<ActualizarUsuarioComando>
    {
        public ActualizarUsuarioValidaccion()
        {
            RuleFor(n => n.Nombre)
                .NotEmpty().WithMessage("El Nombre no puede ser Nulo");

            RuleFor(a => a.Apellido)
                .NotEmpty().WithMessage("El Apellido no puede ser Nulo");
        }
    }
}
