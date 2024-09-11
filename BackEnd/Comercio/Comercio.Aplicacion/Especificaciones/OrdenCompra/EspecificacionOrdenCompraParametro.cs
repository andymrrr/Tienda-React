using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones.OrdenCompra
{
    public class EspecificacionOrdenCompraParametro :EspecificacionParametro
    {
        public int? Id { get; set; }
        public string? Usuario { get; set; }


    }
}
