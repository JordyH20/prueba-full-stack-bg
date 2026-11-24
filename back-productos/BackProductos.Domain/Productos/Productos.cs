namespace BackProductos.Domain.Productos
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Estado { get; set; } = "A";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
    }
}
