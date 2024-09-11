using Comercio.Aplicacion.Funcionalidades.Productos.Comando.RegistrarProducto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto
{
    public class ActualizarProductoValidador : AbstractValidator<ActualizarProductoComando>
    {
        public ActualizarProductoValidador()
        {
            RuleFor(p => p.Nombre)
               .NotEmpty().WithMessage("El Nombre no puede estar en blanco")
               .MaximumLength(50).WithMessage("El Nombre no puede no puede exceder los 50 Caracteres");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La descripcion no puede ser nula");

            RuleFor(p => p.Existencias)
                .NotEmpty().WithMessage("La existencia del producto no puede ser nula");

            RuleFor(p => p.Precio)
                .NotEmpty().WithMessage("El precio no puede ser nulo");
        }
    }
}
