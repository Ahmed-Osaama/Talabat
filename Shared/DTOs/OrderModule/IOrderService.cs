namespace Shared.DTOs.OrderModule
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail);
        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string Email);
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodAsync();
    }
}
