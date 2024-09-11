
using Comercio.Aplicacion.Modelo.Token;
using Comercio.Aplicacion.Servicios.Identidad.Interfaz;
using Comercio.Dominio.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Comercio.Infraestructura.Servicios.Identidad
{
    internal class IdentidadServicio : IIdentidadServicio
    {
        public ConfiguracionJwt _ConfiguracionJwt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentidadServicio(IHttpContextAccessor httpContextAccessor, IOptions<ConfiguracionJwt> ConfiguracionJwt)
        {
            _httpContextAccessor = httpContextAccessor;
            _ConfiguracionJwt = ConfiguracionJwt.Value;
        }
        public string CrearToken(Usuario usuario, IList<string>? roles)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId,usuario.UserName!),
                new Claim("IdUsuario", usuario.Id),
                new Claim("Correo",usuario.Email!)
                };
            foreach (var rol in roles!)
            {
                var claim = new Claim(ClaimTypes.Role, rol);
                claims.Add(claim);
            }
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_ConfiguracionJwt.Llave!));
            var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha512Signature);
            var descripcionToken = new SecurityTokenDescriptor { 
                Subject  = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_ConfiguracionJwt.TiempoExpira),
                SigningCredentials = credenciales
            };
            var tokenManipulador = new JwtSecurityTokenHandler();
            var token = tokenManipulador.CreateToken(descripcionToken);
            return tokenManipulador.WriteToken(token);
        }

        public string ObtenerUsuarioSesion()
        {
            var nombreUsuario = _httpContextAccessor.HttpContext!.User?.Claims?.FirstOrDefault(x=> x.Type == ClaimTypes.NameIdentifier)?.Value;
            return nombreUsuario!;
        }
    }
}
