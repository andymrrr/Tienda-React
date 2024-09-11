using Comercio.Dominio.Modelos;

namespace Comercio.Aplicacion.Especificaciones.OrdenCompra
{
    public class EspecificacionParaContarOrdenCompraParametro : EspecificacionBase<Pedido>
    {
        public EspecificacionParaContarOrdenCompraParametro(EspecificacionOrdenCompraParametro Orden)
             : base(
                   x =>
                    (
                        string.IsNullOrEmpty(Orden.Usuario) ||
                            x.UsuarioComprador!.Contains(Orden.Usuario)) &&
                        (!Orden.Id.HasValue || x.Id == Orden.Id)
                 )
        {
        }
    }
}
