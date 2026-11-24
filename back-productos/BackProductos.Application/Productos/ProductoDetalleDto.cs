namespace BackProductos.Application.Productos;

public class ProductoDetalleDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; }
    public int StockTotal { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public IReadOnlyList<LoteProductoResponseDto> Lotes { get; set; } = [];
}
