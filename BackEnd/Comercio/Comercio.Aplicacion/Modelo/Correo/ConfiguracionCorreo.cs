using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Modelo.Correo
{
    public class ConfiguracionCorreo
    {
        public string? Nombre { get; set; }
        public string? Password { get; set; }
        public int Puerto { get; set; }
        public string? Web { get; set; }
    }
}
