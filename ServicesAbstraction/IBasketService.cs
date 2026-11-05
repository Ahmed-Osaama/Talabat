using Shared.DTOs.BasketModule;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasketAsync(string id);
        public Task<bool> DeleteBasketAsync(string id);
        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
    }
}
