using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Productos;

public class ProductoRequest
{
    [Required(ErrorMessage = "El código es requerido.")]
    [StringLength(50, ErrorMessage = "El código no puede superar 50 caracteres.")]
    [RegularExpression(@"^[A-Za-z0-9\-]{1,50}$", ErrorMessage = "El código solo admite letras, números y guiones.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es requerido.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 150 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ0-9\s\-\.]{2,150}$", ErrorMessage = "El nombre contiene caracteres no permitidos.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no puede superar 500 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [RegularExpression("^[AIN]$", ErrorMessage = "El estado debe ser A, I o N.")]
    public string Estado { get; set; } = "A";
}
