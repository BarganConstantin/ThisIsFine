using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Subscriptions.Commands.Delete.DTOs;

namespace ThiIsFine.Api.Models.Subscriptions.In;

public sealed record DeleteSubscriptionByIdModel(string Id) : IRequestModel<DeleteSubscriptionCommand>
{
    public DeleteSubscriptionCommand Convert() => new(Id);
}
