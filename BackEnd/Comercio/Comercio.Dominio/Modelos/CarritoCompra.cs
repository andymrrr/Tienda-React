using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class CarritoCompra: ModeloBase
    {
        public Guid? IdCarritoCompraMaestro { get; set; }
        public ICollection<CarritoCompraArticulo>? CarritoCompraArticulos { get; set; }
    }
}
