using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Paises.Vm;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Paises.Consulta.BuscarListadoPaises
{
    public class BuscarListadoPaisesHandler : 
        IRequestHandler<BuscarListadoPaisesConsulta, IReadOnlyList< PaisesVm>>
    {
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public BuscarListadoPaisesHandler(IComercioUoW comercio, IMapper mapper)
        {
            _comercio = comercio;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<PaisesVm>> Handle(BuscarListadoPaisesConsulta request, CancellationToken cancellationToken)
        {
            var paises = await _comercio.Repositorio<Pais>().BuscarAsincrono(null, x => x.OrderBy(x => x.Nombre), string.Empty, true);

            var paisesvm = _mapper.Map<IReadOnlyList<PaisesVm>>(paises);

            return paisesvm;
        }
    }
}
