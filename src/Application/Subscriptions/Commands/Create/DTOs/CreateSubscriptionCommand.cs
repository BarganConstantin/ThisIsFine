using Application.Core.Responses;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Application.Subscriptions.Commands.Create.DTOs;

public sealed record CreateSubscriptionCommand(CreateSubscriptionDto CreateSubscriptionDto)
    : IRequest<Result<SubscriptionDto>>;
