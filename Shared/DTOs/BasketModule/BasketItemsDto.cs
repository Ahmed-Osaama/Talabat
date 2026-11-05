using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.BasketModule
{
    public record BasketItemsDto
    {
        public int Id { get; init; } 
        public string ProductName { get; init; } = string.Empty;
        [Range(0 , double.MaxValue)]
        public decimal Price { get; init; }
        public string PictureUrl { get; init; } = string.Empty;
        [Range(0 , 99)]
        public int Quantity { get; init; }
    }
}
