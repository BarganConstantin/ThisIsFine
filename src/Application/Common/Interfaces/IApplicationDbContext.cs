using ThiIsFine.Domain.Entities.User;

namespace ThiIsFine.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<IThisIsFineUser> ThisIsFineUsers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
