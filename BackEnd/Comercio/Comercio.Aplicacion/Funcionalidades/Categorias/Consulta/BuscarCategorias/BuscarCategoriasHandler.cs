using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.Vm;
using Comercio.Aplicacion.Funcionalidades.Paises.Vm;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.BuscarCategorias
{
    public class BuscarCategoriasHandler : IRequestHandler<BuscarCategoriasConsulta, IReadOnlyList<CategoriaVm>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public BuscarCategoriasHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoriaVm>> Handle(BuscarCategoriasConsulta request, CancellationToken cancellationToken)
        {
            var categorias = await _comercio.Repositorio<Categoria>().BuscarAsincrono(null, x => x.OrderBy(x => x.Nombre), string.Empty, true);

            var categoriasvm = _mapper.Map<IReadOnlyList<CategoriaVm>>(categorias);
            return categoriasvm;
        }
    }
}
