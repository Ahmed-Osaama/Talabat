using Domain.Entities.Product;
using Shared;

namespace Services.Specifications
{
    public class ProductsTotalCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductsTotalCountSpecifications(ProductSpecificationsParameters parameters) : base(
            p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
                 (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
                 (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
        }
    }
}
