using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Subscriptions.Queries.GetAll.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.In;

public sealed record GetAllSubscriptionModel : IRequestModel<GetAllSubscriptionsQuery>
{
    public GetAllSubscriptionsQuery Convert() => new();
}
