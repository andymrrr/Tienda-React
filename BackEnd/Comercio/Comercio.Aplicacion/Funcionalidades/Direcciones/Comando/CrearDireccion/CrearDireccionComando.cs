using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Direcciones.Comando.CrearDireccion
{
    public class CrearDireccionComando : IRequest<DireccionVm>
    {
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Departamento { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Pais { get; set; }
    }
}
