using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Modelo.Pago;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Pagos.Comando.CrearPago
{
    public class CrearPagoHandler : IRequestHandler<CrearPagoComando, PedidoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly ConfiguracionStripe _configuracionStripe;
        public CrearPagoHandler(IComercioUoW comercio, IMapper mapper, UserManager<Usuario> userManager, IOptions<ConfiguracionStripe> ConfiguracionStripe)
        {
            _comercio = comercio;
            _mapper = mapper;
            _userManager = userManager;
            _configuracionStripe = ConfiguracionStripe.Value;
        }

        public async Task<PedidoVm> Handle(CrearPagoComando request, CancellationToken cancellationToken)
        {
            var ordenCompra = await _comercio.Repositorio<Pedido>().BuscarEntidadAsincrono(
                d=> d.Id == request.IdPedido,
                null,
                false
                );
            ordenCompra.Estatus = EstatusPedidos.Completada;

            _comercio.Repositorio<Pedido>().ActualizarEntidad(ordenCompra);

            var carritoCompraArticulo = await _comercio.Repositorio<CarritoCompraArticulo>().BuscarAsincrono(
                d => d.IdCarritoCompraMaestro == request.IdCarritoCompraMaestro);

            _comercio.Repositorio<CarritoCompraArticulo>().EliminarRango(carritoCompraArticulo);
            await _comercio.Completo();

            var pedidoVm = _mapper.Map<PedidoVm>(ordenCompra);

            return pedidoVm;
        }
    }
}
