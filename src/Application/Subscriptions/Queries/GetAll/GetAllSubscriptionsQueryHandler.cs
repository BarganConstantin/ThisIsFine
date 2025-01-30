using Application.Core.Responses;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using ThiIsFine.Application.Subscriptions.Queries.GetAll.DTOs;

namespace ThiIsFine.Application.Subscriptions.Queries.GetAll;

public sealed class GetAllSubscriptionsQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
    : IRequestHandler<GetAllSubscriptionsQuery, Result<IEnumerable<SubscriptionDto>>>
{
    public async Task<Result<IEnumerable<SubscriptionDto>>> Handle(
        GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptionsQuery = await applicationUnitOfWork.SubscriptionsRepository.GetAll(cancellationToken);
        var subscriptionDtos = subscriptionsQuery.Select(x => SubscriptionDto.Create(
            x.Id, x.Name, x.Price, x.UsageLimit, x.Description, x.CreationTime));
        
        return Result.Success<IEnumerable<SubscriptionDto>>(subscriptionDtos);
    }
}
