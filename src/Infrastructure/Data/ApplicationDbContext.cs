using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThiIsFine.Domain.Entities.Images;
using ThiIsFine.Domain.Entities.Purchases;
using ThiIsFine.Domain.Entities.Subscriptions;
using ThiIsFine.Domain.Entities.Usages;
using ThiIsFine.Infrastructure.Identity;

namespace ThiIsFine.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ThisIsFineUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ThisIsFineUser> AspNetUsers => Set<ThisIsFineUser>();
    public DbSet<AspNetImage> AspNetImages => Set<AspNetImage>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<Usage> Usages => Set<Usage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
