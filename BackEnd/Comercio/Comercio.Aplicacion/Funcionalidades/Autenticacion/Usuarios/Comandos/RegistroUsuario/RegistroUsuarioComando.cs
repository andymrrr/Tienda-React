﻿using Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Comandos.RegistroUsuario
{
    public class RegistroUsuarioComando : IRequest<AutenticacionRespuesta>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public IFormFile? Foto { get; set; }
        public string? FotoUrl { get; set; }
        public string? FotoId { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }


    }
}
