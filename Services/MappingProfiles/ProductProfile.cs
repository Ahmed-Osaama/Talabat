using AutoMapper;
using Domain.Entities.Product;
using Services.Implementations;
using Shared.DTOs.ProductModule;

namespace Services.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductBrand, BrandResultDto>();
        CreateMap<ProductType, TypeResultDto>();
        CreateMap<Product, ProductResultDto>()
            .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
    }

}
