using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask_Infopulse.DataAccess.Entities;

namespace TestTask_Infopulse.DataAccess.EntityTypeConfigurations
{
    internal class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Category)
                .WithMany(pc => pc.Products)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
