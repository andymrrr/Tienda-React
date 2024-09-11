using Comercio.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Especificaciones.OrdenCompra
{
    public class EspecificacionOrdenCompra : EspecificacionBase<Pedido>
    {
        public EspecificacionOrdenCompra(EspecificacionOrdenCompraParametro Orden) : base(
                   x =>
                    (
                        string.IsNullOrEmpty(Orden.Usuario) ||
                            x.UsuarioComprador!.Contains(Orden.Usuario)) &&
                        (!Orden.Id.HasValue || x.Id == Orden.Id)
                 )
        
        {
            AgregarIncluir(p => p.PedidoArticulos!);

            var skip = Orden.TamanoPagina * (Orden.IndicePagina - 1);
            AplicarPginacion(skip, Orden.TamanoPagina);

            if (!string.IsNullOrEmpty(Orden.Ordenar))
            {
                switch (Orden.Ordenar)
                {
                    case "fechaCreacionAsc":
                        AgregarOrdenarPor(p => p.FechaCreacion!);
                        break;
                    case "fechaCreacionDesc":
                        AgregarOrdenarDescendiente(p => p.FechaCreacion!);
                        break;
                    default:
                        AgregarOrdenarPor(p => p.FechaCreacion!);
                        break;

                }
            }
            else
            {
                AgregarOrdenarDescendiente(f => f.FechaCreacion!);
            }

        }
    }
}
