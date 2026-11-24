using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Productos;

public class ProductoRequest
{
    [Required, StringLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [Required, StringLength(150, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Descripcion { get; set; } = string.Empty;

    [RegularExpression("^[AIN]$", ErrorMessage = "El estado debe ser A, I o N.")]
    public string Estado { get; set; } = "A";
}
