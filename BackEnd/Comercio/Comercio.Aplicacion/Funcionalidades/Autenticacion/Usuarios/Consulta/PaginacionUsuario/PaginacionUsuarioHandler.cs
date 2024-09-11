using AutoMapper;
using Comercio.Aplicacion.Especificaciones.Usuarios;
using Comercio.Aplicacion.Funcionalidades.Compartido;
using Comercio.Aplicacion.Funcionalidades.Productos.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Consulta.PaginacionUsuario
{
    internal class PaginacionUsuarioHandler : IRequestHandler<PaginacionUsuarioConsulta, PaginacionVm<Usuario>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public PaginacionUsuarioHandler(IMapper mapper,IComercioUoW comercio)
        {
            _mapper = mapper;
            _comercio = comercio;
            
        }
        public async Task<PaginacionVm<Usuario>> Handle(PaginacionUsuarioConsulta request, CancellationToken cancellationToken)
        {
            var Parametro = new EspecificacionUsuarioParametro
            {
                IndicePagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                Busqueda = request.Busqueda,
                Ordenar = request.Ordenar,
                
            };
            var especificaciones = new EspecificacionUsuario(Parametro);

            var usuario = await _comercio.Repositorio<Usuario>().BuscarTodaEspecificificaciones(especificaciones);

            var cantidadEspecificaciones = new UsuarioParaContarEspecificaccion(Parametro);
            var totalUsuario = await _comercio.Repositorio<Usuario>().CantidadAsincrona(cantidadEspecificaciones);
            var formulaRedondeo = Convert.ToDecimal(totalUsuario) / Convert.ToDecimal(request.TamanoPagina);
            var redondeado = Math.Ceiling(formulaRedondeo);
            var totalPagina = Convert.ToInt32(redondeado);

            

            var productoPorPagina = usuario.Count();

            var paginacion = new PaginacionVm<Usuario>
            {
                Cantidad = totalUsuario,
                Datos = usuario,
                CantidadPagina = totalPagina,
                Indicepagina = request.IndicePagina,
                TamanoPagina = request.TamanoPagina,
                ResultadoPorPagina = productoPorPagina

            };
            return paginacion;
        }
    }
}
