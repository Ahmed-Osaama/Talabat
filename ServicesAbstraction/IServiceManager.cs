using Domain.Contracts;
using Shared.DTOs.OrderModule;

namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
        IAuthenticationService authenticationService { get; }
        IOrderService OrderService { get; }
        IPaymentService PaymentService { get; }
        ICacheService CacheService { get; }

    }
}
