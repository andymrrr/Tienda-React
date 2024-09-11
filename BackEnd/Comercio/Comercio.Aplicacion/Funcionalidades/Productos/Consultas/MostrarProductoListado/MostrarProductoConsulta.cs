using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using MediatR;


namespace Comercio.Aplicacion.Funcionalidades.Productos.Consultas.MostrarProductoListado
{
    public class MostrarProductoConsulta : IRequest<IReadOnlyList<ProductoVm>>
    {
    }
}
