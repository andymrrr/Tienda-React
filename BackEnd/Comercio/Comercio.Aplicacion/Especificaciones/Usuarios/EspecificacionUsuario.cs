using Comercio.Dominio.Modelos;

namespace Comercio.Aplicacion.Especificaciones.Usuarios
{
    public class EspecificacionUsuario : EspecificacionBase<Usuario>
    {
        public EspecificacionUsuario(EspecificacionUsuarioParametro usuario)  : base(
            b=> (string.IsNullOrEmpty(usuario.Busqueda) || b.Nombre!.Contains(usuario.Busqueda)
                   || b.Apellido!.Contains(usuario.Busqueda)) || b.Email!.Contains(usuario.Busqueda))      
        {

            var formula = usuario.TamanoPagina * (usuario.IndicePagina - 1);
            AplicarPginacion(formula, usuario.TamanoPagina);

            if (!string.IsNullOrEmpty(usuario.Ordenar))
            {
                switch (usuario.Ordenar)
                {
                    case "nombreAsc":
                        AgregarOrdenarPor(p => p.Nombre!);
                        break;
                    case "nombreDesc":
                        AgregarOrdenarDescendiente(p => p.Nombre!);
                        break;
                    case "apellidoAsc":
                        AgregarOrdenarPor(p => p.Apellido!);
                        break;
                    case "apellidoDesc":
                        AgregarOrdenarDescendiente(p => p.Apellido!);
                        break;
                    
                    default:
                        AgregarOrdenarPor(p => p.Nombre!);
                        break;

                }
            }
            else
            {
                AgregarOrdenarDescendiente(f => f.Nombre!);
            }
        }
    }
}
