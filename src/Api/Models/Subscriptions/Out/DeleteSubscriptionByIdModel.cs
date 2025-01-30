using ThiIsFine.Api.Models.Base;
using ThiIsFine.Api.Models.Subscriptions.Base;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.Out;

public sealed record DeleteSubscriptionByIdModel : SubscriptionModel, IResponseModel<SubscriptionDto>
{
    public object? Convert(SubscriptionDto? dto)
    {
        if (dto == null) return default;

        BaseConvert(dto);
        
        return this;
    }
}
