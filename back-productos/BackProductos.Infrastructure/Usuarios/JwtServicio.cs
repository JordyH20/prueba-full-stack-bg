using BackProductos.Application.Usuarios.Ports.Out;
using BackProductos.Domain.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackProductos.Infrastructure.Usuarios
{
    public class JwtServicio : IJwtServicio
    {
        private readonly IConfiguration _configuration;

        public JwtServicio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string Token, DateTime ExpiraEn) GenerarToken(Users usuario)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Falta configurar Jwt:Key.");
            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Falta configurar Jwt:Issuer.");
            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Falta configurar Jwt:Audience.");

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Name, usuario.Nombre!),
                new(ClaimTypes.Email, usuario.Correo!),
                new(ClaimTypes.Role, usuario.RolNombre),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiracion,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return (token, expiracion);
        }
    }
}
