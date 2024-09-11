using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.CarritoCompras.Comando.EliminarArticuloCarritoCompra
{
    public class EliminarArticuloCarritoCompraComando : IRequest<CarritoCompraVm>
    {
        public int Id { get; set; }
    }
}
