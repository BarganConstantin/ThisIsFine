using Application.Core.Responses;
using Application.Core.Services.UserInfo;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using ThiIsFine.Application.Subscriptions.Commands.Delete.DTOs;

namespace ThiIsFine.Application.Subscriptions.Commands.Delete;

public class DeleteSubscriptionCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, 
    ICurrentUserInfo currentUserInfo)
    : IRequestHandler<DeleteSubscriptionCommand, Result<SubscriptionDto>>
{
    public async Task<Result<SubscriptionDto>> Handle(DeleteSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Id)) return Result.BadRequest<SubscriptionDto>("Id is required");
        
        var subscriptionResult = await applicationUnitOfWork
            .SubscriptionsRepository.GetById(request.Id, cancellationToken);
        if (!subscriptionResult.Succeeded) return subscriptionResult.ConvertTo<SubscriptionDto>();
        
        var deleteResult = await applicationUnitOfWork.SubscriptionsRepository.Delete(
            subscriptionResult.Data!, currentUserInfo.Id, cancellationToken);
        await applicationUnitOfWork.SaveAsync(cancellationToken);
        
        return deleteResult.Succeeded
            ? Result.Success(SubscriptionDto.Create(deleteResult.Data!))
            : deleteResult.ConvertTo<SubscriptionDto>();
    }
}
