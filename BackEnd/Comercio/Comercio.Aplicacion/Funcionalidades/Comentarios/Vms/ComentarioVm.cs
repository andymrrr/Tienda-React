using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Comentarios.Vms
{
    public class ComentarioVm
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Clasificacion { get; set; }
        public int IdProducto { get; set; }
        public string? Detatalle { get; set; }

    }
}
