namespace BackProductos.Domain.Usuarios
{
    public class Users
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string ContraseñaHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RolNombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
