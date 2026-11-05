using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.BasketModule;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Basket = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(Basket);
        }
        [HttpPost("Webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];
            await _serviceManager.PaymentService.UpdatePaymentStatusAsync(json, signatureHeader);
            return new EmptyResult();
        }
    }
}
