using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class Pais : ModeloBase
    {
        public string? Nombre { get; set; }
        public string? Iso2 { get; set; }
        public string? Iso3 { get; set; }

    }
}
