using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManger) : ApiController
    {
        //Get orders by id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
        {
            var order = await _serviceManger.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        //Get orders by email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmailAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManger.OrderService.GetOrdersByEmailAsync(Email);
            return Ok(orders);
        }
        //Get DeliveryMethod
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodResult>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await _serviceManger.OrderService.GetDeliveryMethodAsync();
            return Ok(deliveryMethods);
        }
        //Create order
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrder(OrderRequest orderRequest)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManger.OrderService.CreateOrderAsync(orderRequest,Email);
            return Ok(order);
        }
          
    }
}
