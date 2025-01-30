using Domain.Core.Entities;
using ThiIsFine.Domain.Entities.Purchases;

namespace ThiIsFine.Domain.Entities.Subscriptions;

public class Subscription : EntityFullAudited
{
    public string? Name { get; private set; }
    public decimal? Price { get; private set; }
    public int? UsageLimit { get; private set; }
    public string? Description { get; private set; }
    
    public virtual ICollection<Purchase>? Purchases { get; private set; }
    
    public static Subscription Create(string name, decimal price, int usageLimit, string description)
    {
        return new Subscription
        {
            Name = name,
            Price = price,
            UsageLimit = usageLimit,
            Description = description
        };
    }
}

