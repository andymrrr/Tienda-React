using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Consultas.BuscarOrdenCompraId
{
    public class BuscarOrdenCompraIdHandler : IRequestHandler<BuscarOrdenCompraIdConsulta, PedidoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;

        public BuscarOrdenCompraIdHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;

        }
        public async Task<PedidoVm> Handle(BuscarOrdenCompraIdConsulta request, CancellationToken cancellationToken)
        {
            var incluir = new List<Expression<Func< Pedido, object>>>();
            incluir.Add(a => a.PedidoArticulos!.OrderBy(o=> o.Producto));
            incluir.Add(c => c.PedidoDireccion!);

            var pedido = await _comercio.Repositorio<Pedido>().BuscarEntidadAsincrono(
                    x => x.Id == request.IdPedido,
                    incluir,
                    false
                );
            var pedidoVm = _mapper.Map<PedidoVm>(pedido);

            return pedidoVm;
        }
    }
}
