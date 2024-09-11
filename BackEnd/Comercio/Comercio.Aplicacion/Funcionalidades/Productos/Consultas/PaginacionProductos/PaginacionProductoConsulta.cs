using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Dominio.Modelos;
using MediatR;


namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.PaginacionProductos
{
    public class PaginacionProductoConsulta   : ConsultaBasePaginacion, IRequest<PaginacionVm<ProductoVm>>
    {
        public int? IdCategoria { get; set; }
        public int? Clasificacion { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public EstatusProducto? Estatus { get; set; }
    }
}
