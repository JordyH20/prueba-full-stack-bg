using BackProductos.Application.Usuarios.Ports.In;
using BackProductos.Application.Usuarios.Ports.Out;
using BackProductos.Application.Shared.Exceptions;
using BackProductos.Domain.Shared;
using BackProductos.Domain.Usuarios;

namespace BackProductos.Application.Usuarios.UseCases
{
    public class UsuarioUseCase : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioUseCase(IUsuarioRepositorio repositorio, IPasswordHasher passwordHasher)
        {
            _repositorio = repositorio;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioResponseDto> RegistrarAsync(RegistrarUsuarioRequest request)
        {
            var correo = request.Correo.Trim().ToLowerInvariant();
            if (await _repositorio.ExisteCorreoAsync(correo))
                throw new ConflictException("Ya existe un usuario con ese correo.");

            var usuario = new Users
            {
                Nombre = request.Nombre.Trim(),
                Correo = correo,
                ContrasenaHash = _passwordHasher.Hash(request.Contrasena),
                RoleId = (int)Roles.Cliente,
                RolNombre = nameof(Roles.Cliente),
                FechaCreacion = DateTime.UtcNow
            };

            usuario.Id = await _repositorio.CrearAsync(usuario);
            return MapearUsuario(usuario);
        }

        public async Task<IReadOnlyList<UsuarioResponseDto>> ObtenerTodosAsync()
        {
            var usuarios = await _repositorio.ObtenerTodosAsync();
            return usuarios.Select(MapearUsuario).ToArray();
        }

        private static UsuarioResponseDto MapearUsuario(Users usuario) => new()
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre ?? string.Empty,
            Correo = usuario.Correo ?? string.Empty,
            Rol = usuario.RolNombre,
            FechaCreacion = usuario.FechaCreacion
        };
    }
}
