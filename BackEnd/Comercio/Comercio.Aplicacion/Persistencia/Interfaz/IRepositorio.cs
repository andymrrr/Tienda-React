using Comercio.Aplicacion.Especificaciones;
using Stripe.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Persistencia.Interfaz
{
    public interface IRepositorio<T> where T : class
    {
        Task<IReadOnlyList<T>> BuscarTodoAsincrono();

        Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>>? predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarPor,
                                       string? includeString,
                                       bool disableTracking = true);

        Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>>? predicate,
                                       Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarPor = null,
                                       List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);

        Task<T> BuscarEntidadAsincrono(Expression<Func<T, bool>>? predicate,
                                         List<Expression<Func<T, object>>>? includes = null,
                                       bool disableTracking = true);

        Task<T> BuscarPorIdAsincrono(int id);

        Task<T> AgregarAsincrono(T entity);

        Task<T> ActualizarAsincrono(T entity);

        Task EliminarAsincrono(T entity);

        void AgregarEntidad(T entity);

        void ActualizarEntidad(T entity);

        void EliminarEntidad(T entity);

        void AgregarRango(List<T> entities);

        void EliminarRango(IReadOnlyList<T> entities);

        Task<T> BuscarPorIdEspecificaciones(IEspecificacion<T> especificacion);
        Task<IReadOnlyList<T>> BuscarTodaEspecificificaciones(IEspecificacion<T> especificacion);
        Task<int> CantidadAsincrona(IEspecificacion<T> especificacion);

    }
}
