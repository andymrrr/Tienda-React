using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Compartido
{
    public class PaginacionVm<T> where T : class
    {
        public int Cantidad { get; set; }
        public int Indicepagina { get; set; }
        public int TamanoPagina { get; set; }

        public IReadOnlyList<T>? Datos { get; set; }
        public int CantidadPagina { get; set; }

        public int ResultadoPorPagina { get; set; }
    }
}
