using Comercio.Aplicacion.Funcionalidades.Productos.Comando.RegistrarProducto;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto
{
    public class ActualizarProductoComando : IRequest<ProductoVm>
    {

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public decimal? Precio { get; set; }

        public string? Descripcion { get; set; }
        public int? Existencias { get; set; }
        public int IdCategoria { get; set; }
        public string? Vendedor { get; set; }
        public IReadOnlyList<IFormFile>? Fotos { get; set; }
        public IReadOnlyList<RegistrarProductoImagenesComando>? Imagenes { get; set; }

    }
}
