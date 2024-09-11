

using System.Linq.Expressions;

namespace Comercio.Aplicacion.Especificaciones
{
    public class EspecificacionBase<T> : IEspecificacion<T>
    {
        public EspecificacionBase()
        {
                
        }
        public EspecificacionBase(Expression<Func<T, bool>> criterio)
        {
            Criterio = criterio;
        }
        public Expression<Func<T, bool>>? Criterio { get; }

        public List<Expression<Func<T, object>>> Incluir { get; } = new List<Expression<Func<T, Object>>>();

        public Expression<Func<T, object>>? OrdenarPor { get; private set; }

        public Expression<Func<T, object>>? OrdenarDescendente { get; private set; }
        
        public int Tomar { get; private set; }

        public int Saltar { get; private set; }

        public bool habilitarPaginacion { get; private set; }

        protected void AgregarOrdenarPor(Expression<Func<T,object>> ordenarExpresion)
        {
            OrdenarPor = ordenarExpresion;
        }
        protected void AgregarOrdenarDescendiente(Expression<Func<T, object>> ordenarExpresion)
        {
            OrdenarDescendente = ordenarExpresion;
        }
        protected void AplicarPginacion(int saltar, int tomar)
        {
            Tomar = tomar;
            Saltar = saltar;
            habilitarPaginacion = true;
        }
        protected void AgregarIncluir(Expression<Func<T,object>> IncluirExpresion)
        {
           Incluir.Add(IncluirExpresion);
        }
    }
}
