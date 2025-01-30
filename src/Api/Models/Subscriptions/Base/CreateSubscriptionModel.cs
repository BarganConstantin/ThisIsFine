using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.Base;

public record SubscriptionModel
{
    public string? Id { get; protected set; }
    public string? Name { get; protected set; }
    public decimal? Price { get; protected set; }
    public int? UsageLimit { get; protected set; }
    public string? Description { get; protected set; }
    public DateTimeOffset? CreationTime { get; protected set; }
    
    public SubscriptionModel? BaseConvert(SubscriptionDto? dto)
    {
        if (dto == null) return default;
        
        Id = dto.Id;
        Name = dto.Name;
        Price = dto.Price;
        UsageLimit = dto.UsageLimit;
        Description = dto.Description;
        CreationTime = dto.CreationTime;
        
        return this;
    }
}
