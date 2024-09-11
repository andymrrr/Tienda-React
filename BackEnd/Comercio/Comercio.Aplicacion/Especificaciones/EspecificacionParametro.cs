using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones
{
    public class EspecificacionParametro
    {
        public string? Ordenar { get; set; }
        public int IndicePagina { get; set; } = 1;

        public const int TamanoPaginaMaximo = 50;

        private int _TamanoPagina;
        public int TamanoPagina 
        {
            get => _TamanoPagina; 
            set => _TamanoPagina = (value > TamanoPaginaMaximo) ? TamanoPaginaMaximo : value;
        }
        public string? Busqueda { get; set; }
    }
}
