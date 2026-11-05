using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServicesAbstraction;
using Shared.Common;
using Shared.DTOs.OrderModule;

namespace Services.Implementations
{
    public class ServiceManager(Func<IProductService> productServiceFactory ,
        Func<IBasketService> basketServiceFactory ,
        Func<IAuthenticationService> authenticationServiceFactory,
        Func<IOrderService> orderServiceFactory,
        Func<IPaymentService> paymentServiceFactory,
        Func<ICacheService> cacheServiceFactory) : IServiceManager  
    {
        public IProductService ProductService => productServiceFactory.Invoke();

        public IBasketService BasketService => basketServiceFactory.Invoke();

        public IAuthenticationService authenticationService => authenticationServiceFactory.Invoke();

        public IOrderService OrderService => orderServiceFactory.Invoke()   ;

        public IPaymentService PaymentService => paymentServiceFactory.Invoke() ;

        public ICacheService CacheService => cacheServiceFactory.Invoke();
    }
}
