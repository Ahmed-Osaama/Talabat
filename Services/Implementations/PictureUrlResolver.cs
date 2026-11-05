using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities.Product;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.ProductModule;

namespace Services.Implementations
{
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            return $"{configuration.GetSection("URLS")["BaseURL"]}{source.PictureUrl}";
        }
    }
}
