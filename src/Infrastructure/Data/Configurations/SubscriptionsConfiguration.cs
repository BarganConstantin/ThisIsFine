using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThiIsFine.Domain.Entities.Subscriptions;

namespace ThiIsFine.Infrastructure.Data.Configurations;

public sealed class SubscriptionsConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.Property(x => x.Price)
            .HasPrecision(5, 2);

        builder.Property(x => x.Name)
            .HasMaxLength(50);
        
        builder.Property(x => x.Description)
            .HasMaxLength(500);
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
