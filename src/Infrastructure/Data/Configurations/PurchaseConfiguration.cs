using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThiIsFine.Domain.Entities.Purchases;
using ThiIsFine.Infrastructure.Identity;

namespace ThiIsFine.Infrastructure.Data.Configurations;

public sealed class PurchasesConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasOne<ThisIsFineUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId);
        
        builder.HasOne(x => x.Subscription)
            .WithMany(x => x.Purchases)
            .HasForeignKey(x => x.SubscriptionId);
        
        builder.HasMany(x => x.Usages)
            .WithOne(x => x.Purchase)
            .HasForeignKey(x => x.PurchaseId);
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
