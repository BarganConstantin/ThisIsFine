using Application.Core.Responses;
using ThiIsFine.Application.Subscriptions.Base.DTOs;

namespace ThiIsFine.Application.Subscriptions.Queries.GetAll.DTOs;

public sealed record GetAllSubscriptionsQuery : IRequest<Result<IEnumerable<SubscriptionDto>>>;
