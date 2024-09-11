using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Productos.Comando.RegistrarProducto
{
    public class RegistrarProductoHandler : IRequestHandler<RegistrarProductoComando, ProductoVm>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public RegistrarProductoHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<ProductoVm> Handle(RegistrarProductoComando request, CancellationToken cancellationToken)
        {
            var producto = _mapper.Map<Producto>(request);
            var guardar = await _comercio.Repositorio<Producto>().AgregarAsincrono(producto);

            if ((request.Imagenes is not null) && (request.Imagenes.Count > 0))
            {
                request.Imagenes.Select(S => { S.IdProducto = request.Id; return S; }).ToList();
                var imagen = _mapper.Map<List<Imagen>>(request.Imagenes);
                 _comercio.Repositorio<Imagen>().AgregarRango(imagen);
            }

            var productovm = _mapper.Map<ProductoVm>(producto);

            return productovm;
        }
    }
}
