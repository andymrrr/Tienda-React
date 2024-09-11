using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Imagenes.Vms
{
    public class ImagenVm
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public int IdProducto { get; set; }
        public string? CodigoPublco { get; set; }
    }
}
