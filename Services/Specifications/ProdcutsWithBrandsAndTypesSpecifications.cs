using Domain.Entities.Product;
using Shared;
using Shared.Enums;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class ProdcutsWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProdcutsWithBrandsAndTypesSpecifications(ProductSpecificationsParameters parameters) : base(
            p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) && 
                 (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)&&
                 (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);

            switch(parameters.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(x => x.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(x => x.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(x => x.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(x => x.Price);
                    break;
                    default:
                    break;
            }
                ApplyPagination(parameters.MyProperty, parameters.PageIndex);
        }
        public ProdcutsWithBrandsAndTypesSpecifications(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }
    }
}
