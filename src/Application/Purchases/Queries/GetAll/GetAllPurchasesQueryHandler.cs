using Application.Core.Responses;
using ThiIsFine.Application.Purchases.Base;
using ThiIsFine.Application.Purchases.Queries.GetAll.DTOs;
using ThiIsFine.Application.Repositories;

namespace ThiIsFine.Application.Purchases.Queries.GetAll;

public sealed class GetAllPurchasesQueryHandler(IApplicationUnitOfWork applicationUnitOfWork)
    : IRequestHandler<GetAllPurchasesQuery, Result<IEnumerable<PurchaseDto>>>
{
    public async Task<Result<IEnumerable<PurchaseDto>>> Handle(GetAllPurchasesQuery request, 
        CancellationToken cancellationToken)
    {
        var purchasesQuery = await applicationUnitOfWork.PurchasesRepository.GetAll(cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(request.UserId))
            purchasesQuery = purchasesQuery.Where(x => x.UserId == request.UserId);
        
        var purchasesDto = purchasesQuery.Select(x => PurchaseDto.Create(x.Id, x.UserId, x.SubscriptionId, 
            x.Subscription!.Name, x.RemainingAttempts, x.Subscription.UsageLimit, x.Subscription.Price,
            x.CreationTime));
        
        return Result.Success<IEnumerable<PurchaseDto>>(purchasesDto);
    }
}
