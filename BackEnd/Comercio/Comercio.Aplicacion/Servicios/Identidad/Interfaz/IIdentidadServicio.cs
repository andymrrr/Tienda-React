using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Servicios.Identidad.Interfaz
{
    public interface IIdentidadServicio
    {
        string ObtenerUsuarioSesion();
        string CrearToken(Usuario usuario, IList<string>? roles);

    }
}
