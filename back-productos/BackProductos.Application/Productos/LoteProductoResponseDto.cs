namespace BackProductos.Application.Productos;

public class LoteProductoResponseDto
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string NumeroLote { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaIngreso { get; set; }
    public DateTime? FechaActualizacion { get; set; }
}
