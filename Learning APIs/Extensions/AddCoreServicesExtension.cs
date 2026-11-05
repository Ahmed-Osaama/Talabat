using Domain.Contracts;
using Services;
using Services.Implementations;
using ServicesAbstraction;
using Shared.Common;
using Shared.DTOs.OrderModule;

namespace Learning_APIs.Extensions
{
    public static class AddCoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<Func<IProductService>>(serviceProvider => () => serviceProvider.GetRequiredService<IProductService>());
            services.AddScoped<Func<IBasketService>>(serviceProvider => () => serviceProvider.GetRequiredService<IBasketService>());
            services.AddScoped<Func<IAuthenticationService>>(serviceProvider => () => serviceProvider.GetRequiredService<IAuthenticationService>());
            services.AddScoped<Func<IOrderService>>(serviceProvider => () => serviceProvider.GetRequiredService<IOrderService>());
            services.AddScoped<Func<IPaymentService>>(serviceProvider => () => serviceProvider.GetRequiredService<IPaymentService>());
            services.AddScoped<Func<ICacheService>>(serviceProvider => () => serviceProvider.GetRequiredService<ICacheService>());
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
