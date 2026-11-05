using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configurations
{
    public class DeliveryMethodConfigurations: IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,4)");
        }
    }
}
