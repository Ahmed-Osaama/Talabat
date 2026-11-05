using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.ProductModule;
using Shared.Enums;
using Shared.ErrorModels;

namespace Presentation.Controllers
{
    
    public class ProductsController(IServiceManager serviceManager) : ApiController
    {
        [RedisCache(600)]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
            => Ok(await serviceManager.ProductService.GetAllProductsAsync(parameters));
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductResultDto),StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductResultDto?>> GetProductById(int id)
        => Ok(await serviceManager.ProductService.GetProductByIdAsync(id));
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
            => Ok(await serviceManager.ProductService.GetAllBrandsAsync());
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
            => Ok(await serviceManager.ProductService.GetAllTypesAsync());
    }
}
