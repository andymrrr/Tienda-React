using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.ActualizarAdminUsuarioEstatus
{
    public class ActualizarAdminUsuarioEstatusComando: IRequest<Usuario>
    {
        public string? Id { get; set; } 
    }
}
