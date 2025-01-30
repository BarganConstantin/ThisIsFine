using Application.Core.Responses;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using ThiIsFine.Application.Subscriptions.Queries.GetById.DTOs;

namespace ThiIsFine.Application.Subscriptions.Queries.GetById;

public sealed class GetSubscriptionByIdQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
    : IRequestHandler<GetSubscriptionByIdQuery, Result<SubscriptionDto>>
{
    public async Task<Result<SubscriptionDto>> Handle(
        GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Id)) return Result.BadRequest<SubscriptionDto>("Id is required");
        
        var subscriptionResult = await applicationUnitOfWork
            .SubscriptionsRepository.GetById(request.Id, cancellationToken);
        if (!subscriptionResult.Succeeded) return subscriptionResult.ConvertTo<SubscriptionDto>();
        
        return Result.Success<SubscriptionDto>(SubscriptionDto.Create(subscriptionResult.Data!));
    }
}
