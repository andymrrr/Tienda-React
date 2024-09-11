using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.CarritoCompras.Vms;
using Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.Vm;
using Comercio.Aplicacion.Funcionalidades.Comentarios.Vms;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Funcionalidades.Imagenes.Vms;
using Comercio.Aplicacion.Funcionalidades.OrdenCompra.Vms;
using Comercio.Aplicacion.Funcionalidades.Paises.Vm;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.ActualizarProducto;
using Comercio.Aplicacion.Funcionalidades.Productos.Comando.RegistrarProducto;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Dominio.Modelos;

namespace Comercio.Aplicacion.Mapeo
{
    public class MapeoPerfil: Profile
    {
        public MapeoPerfil()
        {
            CreateMap<Producto, ProductoVm>()
                .ForMember(p => p.NombreCategoria, d => d.MapFrom(c => c.Categoria!.Nombre))
                .ForMember(p => p.NumeroComentario, d => d.MapFrom(c => c.Comentarios == null ? 0 : c.Comentarios.Count));

            CreateMap<Imagen, ImagenVm>();
            CreateMap<Comentario, ComentarioVm>();
            CreateMap<Direccion, DireccionVm>().ReverseMap();
            CreateMap<Pais, PaisesVm>();
            CreateMap<Categoria, CategoriaVm>();
            CreateMap<RegistrarProductoComando, Producto>();
            CreateMap<RegistrarProductoImagenesComando, Imagen>();
            CreateMap<ActualizarProductoComando, Producto>();
            CreateMap<CarritoCompra, CarritoCompraVm>()
                .ForMember(C=> C.IdCarritoCompra, d=> d.MapFrom(c=> c.IdCarritoCompraMaestro));
            CreateMap<CarritoCompraArticulo, CarrritoCompraArticulosVm>();
            CreateMap<CarrritoCompraArticulosVm, CarritoCompraArticulo>();

            CreateMap<Pedido,PedidoVm>().ReverseMap();
            CreateMap<PedidoArticulo, PedidoArticuloVm>();
            CreateMap<PedidoDireccion, DireccionVm>();
        }
    }
}
