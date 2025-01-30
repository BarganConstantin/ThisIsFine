using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Subscriptions.Queries.GetById.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.In;

public sealed record GetSubscriptionByIdModel(string Id) : IRequestModel<GetSubscriptionByIdQuery>
{
    public GetSubscriptionByIdQuery Convert() => new(Id);
}
