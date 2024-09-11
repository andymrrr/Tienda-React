using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Roles.Consultas.BuscarRoles
{
    internal class BuscarRolesHandler : IRequestHandler<BuscarRolesConsulta, List<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public BuscarRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<string>> Handle(BuscarRolesConsulta request, CancellationToken cancellationToken)
        {
           var roles = await _roleManager.Roles.ToListAsync();

            return  roles.OrderBy(x => x.Name).Select(s=> s.Name!).ToList<string>(); 
        }
    }
}
