using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask_Infopulse.DataAccess.Entities;

namespace TestTask_Infopulse.DataAccess.EntityTypeConfigurations
{
    internal class OrderEntityConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.TotalCost);
        }
    }
}
