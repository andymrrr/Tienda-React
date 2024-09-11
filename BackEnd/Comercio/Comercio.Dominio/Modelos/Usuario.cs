using Microsoft.AspNetCore.Identity;

namespace Comercio.Dominio.Modelos
{
    public class Usuario: IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? AvatarUrl { get; set; }
        public bool Activo { get; set; } = true;
    }
}
