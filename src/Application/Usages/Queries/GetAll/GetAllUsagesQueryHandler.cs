using Application.Core.Responses;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Application.Usages.Base;
using ThiIsFine.Application.Usages.Queries.GetAll.DTOs;

namespace ThiIsFine.Application.Usages.Queries.GetAll;

public sealed class GetAllUsagesQueryHandler(
    IApplicationUnitOfWork applicationUnitOfWork)
    : IRequestHandler<GetAllUsagesQuery, Result<IEnumerable<UsageDto>>>
{
    public async Task<Result<IEnumerable<UsageDto>>> Handle(GetAllUsagesQuery request, 
        CancellationToken cancellationToken)
    {
        var usagesQuery = (await applicationUnitOfWork.UsagesRepository.GetAll(cancellationToken));
        
        if (!string.IsNullOrWhiteSpace(request.UserId))
            usagesQuery = usagesQuery.Where(x => x.UserId == request.UserId);
            
        var usagesDto = usagesQuery.Select(x => UsageDto.Create(x.Id, x.UserId, x.PurchaseId, 
            x.Purchase!.Subscription!.Name, x.ImageId, x.CreationTime));
        
        return Result.Success<IEnumerable<UsageDto>>(usagesDto);
    }
}
