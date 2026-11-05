using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Exceptions;
using ServicesAbstraction;
using Shared.DTOs.BasketModule;

namespace Services.Implementations
{
    public class BasketService(IBasketRepository _Repository , IMapper _Mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _Mapper.Map<CustomerBasket>(basketDto);
            var CreatedOrUpdatedBasket = await _Repository.CreateOrUpdateBasketAsync(basket);
            return CreatedOrUpdatedBasket is null ? throw new Exception("Can Not Create Or Update The Basket") :
                _Mapper.Map<BasketDto>(CreatedOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        => await _Repository.DeleteBasketAsync(id);

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var Basket = await _Repository.GetBasketAsync(id);
            return Basket is null ? throw new BasketNotFoundException(id) : _Mapper.Map<BasketDto>(Basket);
        }
    }
}
