using ThiIsFine.Application.Purchases.Base;

namespace Web.Models;

public sealed record PurchaseViewModel
{
    public string? Id { get; private set; }
    public string? UserId { get; private set; }
    public string? SubscriptionId { get; private set; }
    public string? SubscriptionName { get; private set; }
    public int? RemainingAttempts { get; private set; }
    public int? UsageLimit { get; private set; }
    public decimal? Price { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    
    public static PurchaseViewModel Create(PurchaseDto purchase)
    {
        return new PurchaseViewModel
        {
            Id = purchase.Id,
            UserId = purchase.UserId,
            SubscriptionId = purchase.SubscriptionId,
            SubscriptionName = purchase.SubscriptionName,
            RemainingAttempts = purchase.RemainingAttempts,
            UsageLimit = purchase.UsageLimit,
            Price = purchase.Price,
            CreatedAt = purchase.CreatedAt
        };
    }
}
