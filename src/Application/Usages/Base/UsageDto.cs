namespace ThiIsFine.Application.Usages.Base;

public sealed record UsageDto
{
    public string? Id { get; private set; }
    public string? UserId { get; private set; }
    public string? PurchaseId { get; private set; }
    public string? SubscriptionName { get; private set; }
    public string? ImageId { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    
    public static UsageDto Create(string? id, string? userId, string? purchaseId, string? subscriptionName, 
        string? imageId, DateTimeOffset? createdAt)
    {
        return new UsageDto
        {
            Id = id,
            UserId = userId,
            PurchaseId = purchaseId,
            SubscriptionName = subscriptionName ?? "Free Trial",
            ImageId = imageId,
            CreatedAt = createdAt
        };
    }
}
