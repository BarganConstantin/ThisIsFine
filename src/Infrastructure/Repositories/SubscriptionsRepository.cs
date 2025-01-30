using Application.Core.Responses;
using Application.Core.Services.DateTimeProvider;
using Microsoft.Extensions.Caching.Memory;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Subscriptions;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Repositories.Generic;

namespace ThiIsFine.Infrastructure.Repositories;

public sealed class SubscriptionsRepository(
    ApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IMemoryCache memoryCache)
    : FullAuditedRepository<Subscription>(dbContext, dateTimeProvider), ISubscriptionsRepository
{
    public override async Task<Result<Subscription>> GetById(string id, CancellationToken cancellationToken)
    {
        if (!memoryCache.TryGetValue(id, out Subscription? subscription))
        {
            var result = await base.GetById(id, cancellationToken);
            if (!result.Succeeded) return result;
            
            subscription = result.Data;
            memoryCache.Set(id, subscription, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10)));

            return result;
        }
        
        return Result.Success(subscription!);
    }

    public override async Task<Result<Subscription>> Delete(Subscription obj, string userId,
        CancellationToken cancellationToken)
    {
        var result = await base.Delete(obj, userId, cancellationToken);
        if (result.Succeeded) memoryCache.Remove(obj.Id);
        
        return result;
    }
}
