using BackProductos.Domain.Usuarios;

namespace BackProductos.Application.Usuarios.Ports.Out
{
    public interface IJwtServicio
    {
        (string Token, DateTime ExpiraEn) GenerarToken(Users usuario);
    }
}
