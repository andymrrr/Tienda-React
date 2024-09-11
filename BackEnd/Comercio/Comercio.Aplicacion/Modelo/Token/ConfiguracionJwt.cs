using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Modelo.Token
{
    public class ConfiguracionJwt
    {
        public string? Llave { get; set; }
        public string? Asunto { get; set; }
        public string? Audiencia { get; set; }
        public decimal DuracionMinuto { get; set; }
        public TimeSpan TiempoExpira { get; set; }

    }
}
