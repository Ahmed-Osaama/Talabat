namespace Domain.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } = string.Empty;
        public List<BasketItem> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
