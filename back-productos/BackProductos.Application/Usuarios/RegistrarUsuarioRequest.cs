using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Usuarios;

public class RegistrarUsuarioRequest
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string Correo { get; set; } = string.Empty;

    [Required, MinLength(8), StringLength(100)]
    public string Contrasena { get; set; } = string.Empty;
}
