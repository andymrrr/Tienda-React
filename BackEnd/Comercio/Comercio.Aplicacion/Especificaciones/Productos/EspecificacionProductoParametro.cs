using Comercio.Dominio.Modelos;

namespace Comercio.Aplicacion.Especificaciones.Productos
{
    public class EspecificacionProductoParametro :EspecificacionParametro
    {
        public int? IdCategoria { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public int? Clasificacion { get; set; }
        public EstatusProducto?  Estatus { get; set; }

    }
}
