using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Usuarios
{
    public class LoginRequestDto
    {
        [Required]
        public string Correo { get; set; } = string.Empty;
        [Required]
        public string Contraseña { get; set; } = string.Empty;
    }
}
