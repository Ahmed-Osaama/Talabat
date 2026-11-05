using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(15,2)");
            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.TypeId);
        }
    }
}
