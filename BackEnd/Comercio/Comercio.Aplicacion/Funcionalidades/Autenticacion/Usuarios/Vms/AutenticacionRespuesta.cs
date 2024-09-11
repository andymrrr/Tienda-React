using Comercio.Aplicacion.Funcionalidades.Direcciones.Vms;
using Stripe;

namespace Comercio.Aplicacion.Funcionalidades.Autenticacion.Usuarios.Vms
{
    public class AutenticacionRespuesta
    {
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Avatar { get; set; }

        public DireccionVm? DireccionEnvio { get; set; }
        public ICollection<string>? Roles { get; set; }
        
    }
}
