namespace BackProductos.Application.Usuarios.Ports.Out;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
