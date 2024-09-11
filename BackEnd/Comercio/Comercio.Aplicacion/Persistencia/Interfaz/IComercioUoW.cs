using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Persistencia.Interfaz
{
    public interface IComercioUoW : IDisposable
    {
        IRepositorio<TEntity> Repositorio<TEntity>() where TEntity : class;
        Task<int> Completo();
    }
}
