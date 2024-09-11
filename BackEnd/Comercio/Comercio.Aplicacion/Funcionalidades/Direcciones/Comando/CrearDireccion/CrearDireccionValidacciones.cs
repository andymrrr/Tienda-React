using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Direcciones.Comando.CrearDireccion
{
    public class CrearDireccionValidacciones : AbstractValidator<CrearDireccionComando>
    {
        public CrearDireccionValidacciones()
        {
            RuleFor(d => d.Ciudad)
                .NotEmpty().WithMessage("La ciudad no puede ser nula");
            RuleFor(d => d.Direccion)
                .NotEmpty().WithMessage("La direccion no puede ser nula");
            RuleFor(d => d.Departamento)
                .NotEmpty().WithMessage("El departamento no puede ser nula");
            RuleFor(d => d.CodigoPostal)
                .NotEmpty().WithMessage("El codigo postal no puede ser nula");
            RuleFor(d => d.Pais)
                    .NotEmpty().WithMessage("El pais no puede ser nula");
        }
    }
}
