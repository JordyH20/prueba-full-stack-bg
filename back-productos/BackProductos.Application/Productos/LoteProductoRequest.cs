using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Productos;

public class LoteProductoRequest
{
    [Required(ErrorMessage = "El número de lote es requerido.")]
    [StringLength(80, ErrorMessage = "El número de lote no puede superar 80 caracteres.")]
    [RegularExpression(@"^[A-Za-z0-9\-]{1,80}$", ErrorMessage = "El número de lote solo admite letras, números y guiones.")]
    public string NumeroLote { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es requerido.")]
    [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0 y no superar 999,999.99.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es requerido.")]
    [Range(0, 999999, ErrorMessage = "El stock debe estar entre 0 y 999,999.")]
    public int Stock { get; set; }
}
