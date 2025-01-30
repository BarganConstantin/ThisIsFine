using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThiIsFine.Infrastructure.Identity;

namespace ThiIsFine.Infrastructure.Data.Configurations;

public sealed class ThisIsFineUserConfiguration : IEntityTypeConfiguration<ThisIsFineUser>
{
    public void Configure(EntityTypeBuilder<ThisIsFineUser> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
