using Comercio.Aplicacion.Funcionalidades.Paises.Vm;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Paises.Consulta.BuscarListadoPaises
{
    public class BuscarListadoPaisesConsulta :IRequest<IReadOnlyList<PaisesVm>>
    {
    }
}
