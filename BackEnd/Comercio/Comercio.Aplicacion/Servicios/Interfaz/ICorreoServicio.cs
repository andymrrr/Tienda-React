using Comercio.Aplicacion.Modelo.Correo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Servicios.Interfaz
{
    public interface ICorreoServicio
    {
        bool EnviarMensaje(MensajeCorreo correo, string token);
    }
}
