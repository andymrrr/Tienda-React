using Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Comando.RegistrarComentario
{
    public class RegistrarComentarioValidador : AbstractValidator<RegistrarComentarioComando>
    {
        public RegistrarComentarioValidador()
        {
            RuleFor(p => p.Nombre)
               .NotEmpty().WithMessage("El Nombre no puede estar en blanco");

            RuleFor(p => p.Detatalle)
                .NotEmpty().WithMessage("El comentario no puede ser nulo");

            RuleFor(p => p.Clasificacion)
                .NotEmpty().WithMessage("La clasificacio del producto no puede ser nula");

            
        }
    }
}
