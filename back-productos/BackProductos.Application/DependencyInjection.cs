using BackProductos.Application.Productos.Ports.In;
using BackProductos.Application.Productos.UseCases;
using BackProductos.Application.Usuarios.Ports.In;
using BackProductos.Application.Usuarios.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BackProductos.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductoServicio, ProductoUseCase>();
            services.AddScoped<IUsuarioServicio, UsuarioUseCase>();
            services.AddScoped<IAuthServicio, AuthUseCase>();
            return services;
        }
    }
}
