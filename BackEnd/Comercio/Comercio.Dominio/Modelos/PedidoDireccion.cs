using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public class PedidoDireccion: ModeloBase
    {
        public string? Direccions { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Usuario { get; set; }
        public string? Pais { get; set; }
    }
}
