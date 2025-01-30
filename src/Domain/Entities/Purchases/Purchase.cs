using Domain.Core.Entities;
using ThiIsFine.Domain.Entities.Subscriptions;
using ThiIsFine.Domain.Entities.Usages;

namespace ThiIsFine.Domain.Entities.Purchases;

public class Purchase : EntityFullAudited
{
    public string? UserId { get; private set; }
    
    public string? SubscriptionId { get; private set; }
    public virtual Subscription? Subscription { get; private set; }
    
    public int? RemainingAttempts { get; private set; }
    
    public virtual ICollection<Usage>? Usages { get; private set; }
    
    public static Purchase Create(string userId, string subscriptionId, int? remainingAttempts)
    {
        return new Purchase
        {
            UserId = userId,
            SubscriptionId = subscriptionId,
            RemainingAttempts = remainingAttempts
        };
    }
    
    public bool HasRemainingAttempts()
    {
        return RemainingAttempts > 0;
    }
    
    public Purchase DecrementRemainingAttempts()
    {
        RemainingAttempts--;
        return this;
    }
}
