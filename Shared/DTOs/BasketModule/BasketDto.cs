namespace Shared.DTOs.BasketModule
{
    public record BasketDto
    {
        public string Id { get; set; } = string.Empty;
        public ICollection<BasketItemsDto> Items { get; set; } = [];
        public string? PaymentIntentId { get; init; }
        public string? ClientSecret { get; init; }
        public decimal? ShippingPrice { get; init; }
        public int? DeliveryMethodId { get; init; }
    }
}
