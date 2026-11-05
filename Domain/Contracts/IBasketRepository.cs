using Domain.Entities.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
            Task<CustomerBasket?> GetBasketAsync(string Id);
            Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket ,TimeSpan? TimeToLive = null );
            Task<bool> DeleteBasketAsync(string Id);
    }
}
