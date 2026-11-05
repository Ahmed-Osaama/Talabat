using AutoMapper;
using Domain.Entities.OrderModule;
using Shared.DTOs.OrderModule;
using ShippingAddress = Domain.Entities.OrderModule.Address;
using UserAddress = Domain.Entities.IdentityModule.Address;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<UserAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>().ForMember(
                dest=>dest.Cost,options => options.MapFrom(src => src.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));
             CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Status,opt =>opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));
        }
    }
}
