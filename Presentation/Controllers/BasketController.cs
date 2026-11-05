using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.BasketModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto dto)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(dto));
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
