using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms
{
    public class CarritoCompraVm
    {
        public string? IdCarritoCompra { get; set; }

        public List<CarrritoCompraArticulosVm>? CarritoCompraArticulos { get; set; }
        public decimal Total {
            get
            {
                return
                 Math.Round(
                        CarritoCompraArticulos!.Sum(x => x.Precio * x.Cantidad) +
                        CarritoCompraArticulos!.Sum(x => x.Precio * x.Cantidad) * Convert.ToDecimal(0.18) +
                        ((CarritoCompraArticulos!.Sum(x => x.Precio * x.Cantidad)) < 100 ? 10 : 25),
                        2
                     );
            }
            set { }
        }
        public int Cantidad {
            get {
                return CarritoCompraArticulos!.Sum(x => x.Cantidad);
            }
            set { }
        }
        public decimal Subtotal {
            get {
                return Math.Round(CarritoCompraArticulos!.Sum(x => x.Precio * x.Cantidad),2);
            }
            set { } 
        }
        public decimal Impuesto {
            get {
                return Math.Round(((CarritoCompraArticulos!.Sum(x=> x.Precio * x.Cantidad))* Convert.ToDecimal(0.18)),2);
            }
            set { }
        }
        public decimal PrecioEnvio {
            get {
                return CarritoCompraArticulos!.Sum(x => x.Precio * x.Cantidad) < 100 ? 10 : 25;
            }
            set { }
        }

    }
}
