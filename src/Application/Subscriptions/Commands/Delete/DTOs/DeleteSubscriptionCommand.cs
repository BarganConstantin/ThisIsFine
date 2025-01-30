using ThiIsFine.Application.Base.BaseById;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Application.Subscriptions.Commands.Delete.DTOs;

public sealed record DeleteSubscriptionCommand : BaseByIdRequest<SubscriptionDto>
{
    public DeleteSubscriptionCommand(string id) : base(id) { }
}
