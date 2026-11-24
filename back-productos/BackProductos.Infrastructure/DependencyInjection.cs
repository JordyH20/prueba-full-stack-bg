using BackProductos.Application.Usuarios.Ports.Out;
using BackProductos.Application.Productos.Ports.Out;
using BackProductos.Infrastructure.Shared;
using BackProductos.Infrastructure.Productos;
using BackProductos.Infrastructure.Usuarios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackProductos.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DapperData>();
            services.AddScoped<IJwtServicio, JwtServicio>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            return services;
        }
    }
}
