using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones.Usuarios
{
    public class UsuarioParaContarEspecificaccion :EspecificacionBase<Usuario>
    {
        public UsuarioParaContarEspecificaccion(EspecificacionUsuarioParametro usuario) : base(
            b => (string.IsNullOrEmpty(usuario.Busqueda) || b.Nombre!.Contains(usuario.Busqueda)
                   || b.Apellido!.Contains(usuario.Busqueda)) || b.Email!.Contains(usuario.Busqueda))
        {
            
        }
    }
}
