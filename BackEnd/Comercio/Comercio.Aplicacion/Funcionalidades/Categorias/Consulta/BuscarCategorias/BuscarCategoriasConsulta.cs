﻿using Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.Vm;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Categorias.Consulta.BuscarCategorias
{
    public class BuscarCategoriasConsulta : IRequest<IReadOnlyList<CategoriaVm>>
    {
    }
}