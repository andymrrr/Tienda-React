using Comercio.Aplicacion.Especificaciones;
using Microsoft.EntityFrameworkCore;

namespace Comercio.Infraestructura.Especificaciones
{
    public class EvaluadorEspecificaciones<T> where T : class
    {
        public static IQueryable<T> MostrarConsulta(IQueryable<T> consulta, IEspecificacion<T> especificacion)
        {
            if(especificacion.Criterio != null)
            {
                consulta = consulta.Where(especificacion.Criterio);
            }
            if (especificacion.OrdenarPor != null)
            {
                consulta = consulta.OrderBy(especificacion.OrdenarPor);
            }
            if(especificacion.OrdenarDescendente != null)
            {
                consulta = consulta.OrderBy(especificacion.OrdenarDescendente);
            }
            if (especificacion.habilitarPaginacion)
            {
                consulta = consulta.Skip(especificacion.Saltar).Take(especificacion.Tomar);
            }
             consulta = especificacion.Incluir!.Aggregate(consulta, (current, include )=> current.Include(include)).AsSplitQuery().AsNoTracking();

             
            return consulta;
        }
    }
}

