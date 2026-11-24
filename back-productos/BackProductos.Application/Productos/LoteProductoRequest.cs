using System.ComponentModel.DataAnnotations;

namespace BackProductos.Application.Productos;

public class LoteProductoRequest
{
    [Required, StringLength(80)]
    public string NumeroLote { get; set; } = string.Empty;

    [Range(0.01, 999999999)]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
