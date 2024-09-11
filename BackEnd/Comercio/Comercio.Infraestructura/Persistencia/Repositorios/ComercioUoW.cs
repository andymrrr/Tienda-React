using Comercio.Aplicacion.Persistencia.Interfaz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Persistencia.Repositorios
{
    public class ComercioUoW : IComercioUoW
    {
        private Hashtable? _repositorio;

        private readonly DbContextComercio _dbContext;
        public ComercioUoW(DbContextComercio dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Completo()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error en Transaccion ", ex);
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepositorio<TEntity> Repositorio<TEntity>() where TEntity : class
        {
            if (_repositorio is null)
            {
                _repositorio = new Hashtable();
            }
            var tipo = typeof(TEntity).Name;
            if(!_repositorio.ContainsKey(tipo))
            {
                var tipoRepositorio = typeof(Repositorio<>);
                var instanciaReppositorio = Activator.CreateInstance(tipoRepositorio.MakeGenericType(typeof(TEntity)),_dbContext);
                _repositorio.Add(tipo,instanciaReppositorio);
            }
            return (IRepositorio<TEntity>)_repositorio[tipo]!;
        }
    }
}
