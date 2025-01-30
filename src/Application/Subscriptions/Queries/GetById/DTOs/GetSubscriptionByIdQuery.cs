using ThiIsFine.Application.Base.BaseById;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Application.Subscriptions.Queries.GetById.DTOs;

public sealed record GetSubscriptionByIdQuery : BaseByIdRequest<SubscriptionDto>
{
    public GetSubscriptionByIdQuery(string? id) : base(id) {}
}
