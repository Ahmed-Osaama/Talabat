namespace Domain.Entities.Product
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
