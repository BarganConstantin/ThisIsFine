using ThiIsFine.Api.Models.Base;
using ThiIsFine.Api.Models.Subscriptions.Base;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.Out;

public sealed record GetAllSubscriptionModel : IResponseModel<IEnumerable<SubscriptionDto>>
{
    public IEnumerable<SubscriptionModel?>? Subscriptions { get; set; }
    
    public object? Convert(IEnumerable<SubscriptionDto>? dto)
    {
        if (dto == null) return default;

        Subscriptions = dto.Select(x => new SubscriptionModel().BaseConvert(x));
        
        return this;
    }
}
