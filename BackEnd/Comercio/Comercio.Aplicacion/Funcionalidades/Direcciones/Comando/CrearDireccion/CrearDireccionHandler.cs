using AutoMapper;
using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Comercio.Aplicacion.Persistencia.Interfaz;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Direcciones.Comando.CrearDireccion
{
    public class CrearDireccionHandler : IRequestHandler<CrearDireccionComando, DireccionVm>
    {
        private readonly IIdentidadServicio _identidadServicio;
        private readonly IComercioUoW _comercio;
        private readonly IMapper _mapper;
        public CrearDireccionHandler(IIdentidadServicio identidadServicio,IComercioUoW comercio, IMapper mapper)
        {
            _identidadServicio = identidadServicio;
            _comercio = comercio;
            _mapper = mapper;
        }
        public async Task<DireccionVm> Handle(CrearDireccionComando request, CancellationToken cancellationToken)
        {
            var direccion = await _comercio.Repositorio<Direccion>().BuscarEntidadAsincrono(
                x => x.Usuario == _identidadServicio.ObtenerUsuarioSesion(),
                null,
                false);

            if(direccion is null)
            {
                direccion = new Direccion
                {
                    Direccions = request.Direccion,
                    Ciudad = request.Ciudad,
                    Departamento = request.Departamento,
                    CodigoPostal = request.CodigoPostal,
                    Pais = request.Pais,
                    Usuario =_identidadServicio.ObtenerUsuarioSesion()

                };
                _comercio.Repositorio<Direccion>().AgregarEntidad(direccion);
            }
            else
            {
                direccion.Direccions = request.Direccion;
                direccion.Ciudad = request.Ciudad;
                direccion.Departamento = request.Departamento;
                direccion.Pais = request.Pais;
                direccion.Usuario = _identidadServicio.ObtenerUsuarioSesion();
            }
            await _comercio.Completo();

            var direccionVm = _mapper.Map<DireccionVm>(direccion);

            return direccionVm;
        }
    }
}
