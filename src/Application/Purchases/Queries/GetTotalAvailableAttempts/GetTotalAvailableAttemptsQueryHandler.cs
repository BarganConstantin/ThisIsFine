using Application.Core.Responses;
using Application.Core.Services.Identity;
using Application.Core.Services.UserInfo;
using ThiIsFine.Application.Purchases.Queries.GetTotalAvailableAttempts.DTOs;
using ThiIsFine.Application.Repositories;

namespace ThiIsFine.Application.Purchases.Queries.GetTotalAvailableAttempts;

public sealed class GetTotalAvailableAttemptsQueryHandler(IApplicationUnitOfWork applicationUnitOfWork, 
    ICurrentUserInfo currentUserInfo, IIdentityService identityService) 
    : IRequestHandler<GetTotalAvailableAttemptsQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetTotalAvailableAttemptsQuery request, CancellationToken cancellationToken)
    {
        var purchasesQuery = await applicationUnitOfWork.PurchasesRepository.GetAll(cancellationToken);
        
        var totalAvailableAttempts = purchasesQuery
            .Where(x => x.UserId == currentUserInfo.Id)
            .Sum(x => x.RemainingAttempts);
        
        var userDto = await identityService.GetUserById(currentUserInfo.Id, cancellationToken);
        if (!userDto.Succeeded) return userDto.ConvertTo<int>();
        
        return Result.Success(totalAvailableAttempts + userDto.Data!.FreeTrialAttempts ?? 0);
    }
}
