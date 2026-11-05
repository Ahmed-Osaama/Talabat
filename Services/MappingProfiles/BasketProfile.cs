using AutoMapper;
using Domain.Entities.Basket;
using Shared.DTOs.BasketModule;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto,CustomerBasket>().ReverseMap();
            CreateMap<BasketItem,BasketItemsDto>().ReverseMap();
        }
    }
}
