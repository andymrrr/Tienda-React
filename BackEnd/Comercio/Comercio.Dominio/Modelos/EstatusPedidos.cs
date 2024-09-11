using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Dominio.Modelos
{
    public enum EstatusPedidos
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,
        [EnumMember(Value = "El Pago Fue Recibido ")]
        Completada,
        [EnumMember(Value = "El Producto Fue Enviado ")]
        Enviado,
        [EnumMember(Value = "El Pago Tuvo Errores ")]
        Error
    }
}
