namespace Shared.DTOs.OrderModule
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public AddressDto ShipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
