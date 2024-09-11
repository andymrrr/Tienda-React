using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.ActualizarOrdenCompra
{
    public class ActualizarOrdenCompraHandler : IRequestHandler<ActualizarOrdenCompraComando, PedidoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
       
        public ActualizarOrdenCompraHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;

        }
        public async Task<PedidoVm> Handle(ActualizarOrdenCompraComando request, CancellationToken cancellationToken)
        {
            var pedido = await _comercio.Repositorio<Pedido>().BuscarPorIdAsincrono(request.IdPedido);

            if(pedido is null)
            {
                throw new Exception("El Pedio no existe");
            }
            pedido.Estatus = request.Estatus;

            _comercio.Repositorio<Pedido>().ActualizarEntidad(pedido);
            var resultado = await _comercio.Completo();
            if (resultado <0)
            {
                throw new Exception("No se pudo actualizar la orden de compra");
            }

            var productoVm = _mapper.Map<PedidoVm>(pedido);
            return productoVm;
        }
    }
}
