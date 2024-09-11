using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public enum EstatusProducto
    {
        [EnumMember(Value ="Producto Inactivo")]
        Inactivo,

        [EnumMember(Value = "Producto activo")]
        Activo
    }
}
