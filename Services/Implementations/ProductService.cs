using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Product;
using Domain.Exceptions;
using Services.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.ProductModule;
using Shared.Enums;

namespace Services.Implementations
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
              return mapper.Map<IEnumerable<BrandResultDto>>(Brands);
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
        {
            var ProductRepo = unitOfWork.GetRepository<Product, int>();
            var specification = new ProdcutsWithBrandsAndTypesSpecifications(parameters);
            var Products = await ProductRepo.GetAllAsync(specification);
            var countSpecification = new ProductsTotalCountSpecifications(parameters);
            var TotalCount = await ProductRepo.CountAsync(countSpecification);
            var ProductsResult = mapper.Map<IEnumerable<ProductResultDto>>(Products);
            var PageSize = ProductsResult.Count();
            return new PaginatedResult<ProductResultDto>(parameters.PageIndex, PageSize, TotalCount, ProductsResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeResultDto>>(Types);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var specification = new ProdcutsWithBrandsAndTypesSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            return product is null ? throw new ProductNotFoundException(id) : mapper.Map<ProductResultDto?>(product);
        }
    }
}
