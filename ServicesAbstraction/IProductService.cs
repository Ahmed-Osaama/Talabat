using Shared;
using Shared.DTOs.ProductModule;
using Shared.Enums;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        Task<ProductResultDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
