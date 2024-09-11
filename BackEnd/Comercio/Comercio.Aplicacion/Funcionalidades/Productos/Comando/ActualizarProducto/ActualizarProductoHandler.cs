//using AutoMapper;
//using Comercio.Aplicacion.Excepciones;
//using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
//using Comercio.Aplicacion.Persistencia.Interfaz;
//using Comercio.Dominio.Modelos;
//using FluentValidation;
//using MediatR;

//namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto
//{
//    public class ActualizarProductoHandler : IRequestHandler<ActualizarProductoComando, ProductoVm>
//    {
//        private readonly IComercioUoW _comercio;
//        private readonly IMapper _mapper;
//        private readonly IValidator<ActualizarProductoComando> _validador;
//        public ActualizarProductoHandler( IValidator<ActualizarProductoComando> validador ,IComercioUoW comercio, IMapper mapper)
//        {
//            _comercio = comercio;
//            _mapper = mapper;
//            _validador = validador;
//        }
//        public async Task<ProductoVm> Handle(ActualizarProductoComando request, CancellationToken cancellationToken)
//        {
//            var producto = await _comercio.Repositorio<Producto>().BuscarPorIdAsincrono(request.Id);
//            if(producto is null)
//            {
//                throw new NotFoundException(nameof(producto), request.Id);
//            }
//            _mapper.Map(request, producto, typeof(ActualizarProductoComando), typeof(Producto));
//            await _comercio.Repositorio<Producto>().ActualizarAsincrono(producto);

//            if((request.Imagenes is not null) && (request.Imagenes.Count >0))
//            {
//                var imagenEliminada = await _comercio.Repositorio<Imagen>().BuscarAsincrono(x=> x.IdProducto == request.Id); 

//                _comercio.Repositorio<Imagen>().EliminarRango(imagenEliminada);

//                request.Imagenes.Select(s => { s.IdProducto = request.Id; return s; });

//                var imagen = _mapper.Map<List<Imagen>>(request.Imagenes);
//                _comercio.Repositorio<Imagen>().AgregarRango(imagen);

//                await _comercio.Completo();
//            }

//            var productovm = _mapper.Map<ProductoVm>(producto);

//            return productovm;
          
//        }
//    }
//}
