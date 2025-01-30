using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThiIsFine.Domain.Entities.Usages;

namespace ThiIsFine.Infrastructure.Data.Configurations;

public sealed class UsagesConfiguration : IEntityTypeConfiguration<Usage>
{
    public void Configure(EntityTypeBuilder<Usage> builder)
    {
        builder.HasOne(x => x.Image)
            .WithOne()
            .HasForeignKey<Usage>(x => x.ImageId);
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
