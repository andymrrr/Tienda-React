using Comercio.Aplicacion.Especificaciones;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Infraestructura.Especificaciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Persistencia.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly DbContextComercio _dbContext;
        public Repositorio(DbContextComercio dbContext)
        {   
            _dbContext = dbContext;
        }
        public async Task<T> ActualizarAsincrono(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        public void ActualizarEntidad(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> AgregarAsincrono(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public void AgregarEntidad(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void AgregarRango(List<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public async Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarpor, string? incluircadena, bool desactivarseguimiento = true)
        {
            IQueryable<T> consulta = _dbContext.Set<T>();
            if (desactivarseguimiento)
                consulta = consulta.AsNoTracking();
            if (!string.IsNullOrEmpty(incluircadena))
                consulta = consulta.Include(incluircadena);
            if (predicate != null)
                consulta = consulta.Where(predicate);
            if (ordenarpor != null)
               return await  ordenarpor(consulta).ToListAsync();

            return await consulta.ToListAsync();

        }

        public async Task<IReadOnlyList<T>> BuscarAsincrono(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? ordenarpor = null, List<Expression<Func<T, object>>>? includes = null, bool desactivarseguimiento = true)
        {
            IQueryable<T> consulta = _dbContext.Set<T>();
            if(desactivarseguimiento)
                consulta= consulta.AsNoTracking();
            if (includes != null)
                consulta = includes.Aggregate(consulta, (actual, include) => actual.Include(include));
            if (predicate != null)
                consulta = consulta.Where(predicate);
            if (ordenarpor != null)
                return await ordenarpor(consulta).ToListAsync();

            return await consulta.ToListAsync();
        }

        public async Task<T> BuscarPorIdAsincrono(int id)
        {
            return (await _dbContext.Set<T>().FindAsync(id))!; 
        }

       

        public async Task<IReadOnlyList<T>> BuscarTodoAsincrono()
            {
            return await _dbContext.Set<T>().ToListAsync();
        }

       

        public async Task EliminarAsincrono(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        public void EliminarEntidad(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void EliminarRango(IReadOnlyList<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<T> BuscarEntidadAsincrono(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool desactivarseguimiento = true)
        {
            IQueryable<T> consulta = _dbContext.Set<T>();
            if (desactivarseguimiento)
                consulta = consulta.AsNoTracking();
            if (includes != null)
                consulta = includes.Aggregate(consulta, (actual, include) => actual.Include(include));
            if(predicate != null)
                consulta = consulta.Where(predicate);
            return (await consulta.FirstOrDefaultAsync())!;

        }

        public async Task<T> BuscarPorIdEspecificaciones(IEspecificacion<T> especificacion)
        {
            return (await AplicarEspecificaciones(especificacion).FirstOrDefaultAsync())!;
        }

        public async Task<IReadOnlyList<T>> BuscarTodaEspecificificaciones(IEspecificacion<T> especificacion)
        {
            return await AplicarEspecificaciones(especificacion).ToListAsync();
        }

        public async Task<int> CantidadAsincrona(IEspecificacion<T> especificacion)
        {
            return await AplicarEspecificaciones(especificacion).CountAsync();
        }

        public IQueryable<T> AplicarEspecificaciones(IEspecificacion<T> especificacion)
        {
            return EvaluadorEspecificaciones<T>.MostrarConsulta(_dbContext.Set<T>().AsQueryable(), especificacion);
        }
    }
}
