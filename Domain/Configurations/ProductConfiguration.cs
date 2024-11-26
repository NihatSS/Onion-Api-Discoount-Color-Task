using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Quantity).HasDefaultValue(0);
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.Price).IsRequired();
            
        }
    }
}
