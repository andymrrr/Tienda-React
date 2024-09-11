using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones
{
    public interface IEspecificacion<T>
    {
        Expression<Func<T, bool>>? Criterio { get; }

        List<Expression<Func<T, object>>>? Incluir { get; }

        Expression<Func<T, object>>? OrdenarPor { get; }

        Expression<Func<T, object>>? OrdenarDescendente { get; }

        int Tomar { get; }
        int Saltar { get; }    
        bool habilitarPaginacion { get; }
    }
}
