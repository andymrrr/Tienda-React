using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.BuscarProductoPorId
{
    public class BuscarProductoPorIdConsulta : IRequest<ProductoVm>
    {
        public int IdProducto { get; set; }

        public BuscarProductoPorIdConsulta(int idproducto)
        {
            IdProducto = idproducto == 0? throw new ArgumentNullException(nameof(idproducto)) : idproducto;

        }
    }
}
