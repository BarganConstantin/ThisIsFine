using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace Web.Models;

public sealed record SubscriptionViewModel
{
    public string? Id { get; private set; }
    public string? Name { get; private set; }
    public decimal? Price { get; private set; }
    public int? UsageLimit { get; private set; }
    public string? Description { get; private set; }
    
    public static SubscriptionViewModel Create(SubscriptionDto subscriptionDto)
    {
        return new SubscriptionViewModel
        {
            Id = subscriptionDto.Id,
            Name = subscriptionDto.Name,
            Price = subscriptionDto.Price,
            UsageLimit = subscriptionDto.UsageLimit,
            Description = subscriptionDto.Description
        };
    }
}
