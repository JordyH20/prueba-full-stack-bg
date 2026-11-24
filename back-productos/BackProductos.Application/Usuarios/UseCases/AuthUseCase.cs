using BackProductos.Application.Usuarios.Ports.In;
using BackProductos.Application.Usuarios.Ports.Out;

namespace BackProductos.Application.Usuarios.UseCases;

public sealed class AuthUseCase(
    IUsuarioRepositorio usuarioRepositorio,
    IPasswordHasher passwordHasher,
    IJwtServicio jwtServicio) : IAuthServicio
{
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var usuario = await usuarioRepositorio.ObtenerPorCorreoAsync(request.Correo.Trim());
        if (usuario is null || !passwordHasher.Verify(request.Contrasena, usuario.ContrasenaHash))
            throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");

        var (token, expiracion) = jwtServicio.GenerarToken(usuario);
        return new LoginResponseDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre ?? string.Empty,
            Correo = usuario.Correo ?? string.Empty,
            Rol = usuario.RolNombre,
            Token = token,
            ExpiraEn = expiracion
        };
    }
}
