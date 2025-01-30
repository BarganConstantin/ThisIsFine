using ThiIsFine.Domain.Entities.Subscriptions;

namespace ThiIsFine.Application.Subscriptions.Base.DTOs;

public sealed record SubscriptionDto
{
    public string? Id { get; private set; }
    public string? Name { get; private set; }
    public decimal? Price { get; private set; }
    public int? UsageLimit { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreationTime { get; private set; }
    
    public static SubscriptionDto Create(Subscription subscription)
    {
        return new SubscriptionDto
        {
            Id = subscription.Id,
            Name = subscription.Name,
            Price = subscription.Price,
            UsageLimit = subscription.UsageLimit,
            Description = subscription.Description,
            CreationTime = subscription.CreationTime
        };
    }

    public static SubscriptionDto Create(string? id, string? name, decimal? price, int? usageLimit, string? description,
        DateTimeOffset? creationTime)
    {
        return new SubscriptionDto
        {
            Id = id,
            Name = name,
            Price = price,
            UsageLimit = usageLimit,
            Description = description,
            CreationTime = creationTime
        };
    }
}
