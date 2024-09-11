using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public abstract class ModeloBase
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public string? ModificadoPor { get; set; }


    }
}
