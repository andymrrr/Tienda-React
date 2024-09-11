using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Modelo.Pago;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Linq.Expressions;

namespace Comercio.Aplicacion.Funcionalidades.OrdenCompra.Comando.CrearOrdenCompra
{
    public class CrearOrdenCompraHandler : IRequestHandler<CrearOrdenCompraComando, PedidoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IIdentidadServicio _identidadServicio;
        private readonly ConfiguracionStripe _configuracionStripe;
        public CrearOrdenCompraHandler(IComercioUoW comercio, IMapper mapper, UserManager<Usuario> userManager, IIdentidadServicio identidadServicio, IOptions<ConfiguracionStripe> ConfiguracionStripe)
        {
            _comercio = comercio;
            _mapper = mapper;
            _userManager = userManager;
            _identidadServicio = identidadServicio;
            _configuracionStripe = ConfiguracionStripe.Value;
        }
        public async Task<PedidoVm> Handle(CrearOrdenCompraComando request, CancellationToken cancellationToken)
        {

            var pedidosPendiente = await _comercio.Repositorio<Pedido>().BuscarEntidadAsincrono(d =>
            d.UsuarioComprador == _identidadServicio.ObtenerUsuarioSesion() && d.Estatus == EstatusPedidos.Pendiente,
            null,
            true);

            if (pedidosPendiente is not null)
            {
                await _comercio.Repositorio<Pedido>().EliminarAsincrono(pedidosPendiente);
            }
            var incluir = new List<Expression<Func<CarritoCompra, object>>>();
            incluir.Add(C => C.CarritoCompraArticulos!.OrderBy(o => o.NombreProducto));

            var carrito = await _comercio.Repositorio<CarritoCompra>().BuscarEntidadAsincrono(
                    x => x.IdCarritoCompraMaestro == request.IdCarritoCompra,
                    incluir,
                    false
                );

            var usuario = await _userManager.FindByNameAsync(_identidadServicio.ObtenerUsuarioSesion());
            if (usuario is  null)
            {
                throw new Exception("El usuario no existe");
            }
            var direccion = await _comercio.Repositorio<Direccion>().BuscarEntidadAsincrono(
                d => d.Usuario == _identidadServicio.ObtenerUsuarioSesion(),
                null,
                false
                );

            PedidoDireccion pedidoDireccion = new()
            {
                Direccions = direccion.Direccions,
                Ciudad = direccion.Ciudad,
                CodigoPostal = direccion.CodigoPostal,
                Pais = direccion.Pais,
                Departamento = direccion.Departamento,
                Usuario = direccion.Usuario
            };
            await _comercio.Repositorio<PedidoDireccion>().AgregarAsincrono(pedidoDireccion);

            var subtotal = Math.Round(carrito.CarritoCompraArticulos!.Sum(s => s.Precio * s.Cantidad), 2);

            var impuesto = Math.Round(subtotal * Convert.ToDecimal(0.18), 2);
            var precioEnvio = subtotal < 100 ? 10 : 25;
            var total =Math.Round(subtotal + impuesto + precioEnvio,2);

            var nombreComprador = $"{usuario!.Nombre} {usuario.Apellido}";

            var pedido = new Pedido(nombreComprador, usuario.UserName, pedidoDireccion, subtotal, total, impuesto, precioEnvio);

            
            await _comercio.Repositorio<Pedido>().AgregarAsincrono(pedido);

            var articulos = new List<PedidoArticulo>();

            foreach (var carritoArticulo in carrito.CarritoCompraArticulos!)
            {
                var pedidoArticulo = new PedidoArticulo
                {
                    NombreProducto = carritoArticulo.NombreProducto,
                    IdProducto = carritoArticulo.IdProducto,
                    ImagenUrl = carritoArticulo.Imagen,
                    Precio = carritoArticulo.Precio,
                    Cantidad = carritoArticulo.Cantidad,
                    IdPedido = carritoArticulo.Id

                };
                articulos.Add(pedidoArticulo);
            }

            _comercio.Repositorio<PedidoArticulo>().AgregarRango(articulos);
            var resultado = await _comercio.Completo();
            if (resultado < 0)
            {
                throw new Exception("Error Creando la orden de compra");
            }
            #region Stripe Pasarela de Pago

            StripeConfiguration.ApiKey = _configuracionStripe.LlaveSecreta;
            var servicio = new PaymentIntentService();
            PaymentIntent IntentoPago;

            if(string.IsNullOrEmpty(pedido.IdIntencionPago))
            {
                var opciones = new PaymentIntentCreateOptions
                {
                    Amount = (long)pedido.Total,
                    Currency = Moneda.usd,
                    PaymentMethodTypes = new List<string> { TipoPago.card }
                    
                };

                IntentoPago = await servicio.CreateAsync(opciones);
                pedido.IdIntencionPago = IntentoPago.Id;
                pedido.SecretoCliente = IntentoPago.ClientSecret;
                pedido.StripeApiKey = _configuracionStripe.LlavePublica;
            }
            else
            {
                var actuualizaropcion = new PaymentIntentUpdateOptions
                {
                    Amount = (long)pedido.Total
                };

                await servicio.UpdateAsync(pedido.IdIntencionPago,actuualizaropcion);
            }
            #endregion

            _comercio.Repositorio<Pedido>().ActualizarEntidad(pedido);

            var resultadoPedido =await _comercio.Completo();
            if(resultadoPedido < 0)
            {
                throw new Exception("Error Actualizando la orden de compra");

            }

            var PedidoVm = _mapper.Map<PedidoVm>(pedido);

            return PedidoVm;




        }
    }
}
