using ThiIsFine.Domain.Entities.Purchases;

namespace ThiIsFine.Application.Purchases.Base;

public sealed record PurchaseDto
{
    public string? Id { get; private set; }
    public string? UserId { get; private set; }
    public string? SubscriptionId { get; private set; }
    public string? SubscriptionName { get; private set; }
    public int? RemainingAttempts { get; private set; }
    public int? UsageLimit { get; private set; }
    public decimal? Price { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    
    public static PurchaseDto Create(Purchase purchase)
    {
        return new PurchaseDto
        {
            Id = purchase.Id,
            UserId = purchase.UserId,
            SubscriptionId = purchase.SubscriptionId,
            SubscriptionName = purchase.Subscription?.Name,
            RemainingAttempts = purchase.RemainingAttempts,
            UsageLimit = purchase.Subscription?.UsageLimit,
            Price = purchase.Subscription?.Price,
            CreatedAt = purchase.CreationTime
        };
    }
    
    public static PurchaseDto Create(string? id, string? userId, string? subscriptionId, string? subscriptionName, 
        int? remainingAttempts, int? usageLimit, decimal? price, DateTimeOffset? createdAt)
    {
        return new PurchaseDto
        {
            Id = id,
            UserId = userId,
            SubscriptionId = subscriptionId,
            SubscriptionName = subscriptionName,
            RemainingAttempts = remainingAttempts,
            UsageLimit = usageLimit,
            Price = price,
            CreatedAt = createdAt
        };
    }
}
