using Application.Core.Responses;
using Application.Core.Services.UserInfo;
using ThiIsFine.Application.Purchases.Base;
using ThiIsFine.Application.Purchases.Create.DTOs;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Purchases;

namespace ThiIsFine.Application.Purchases.Create;

public class CreatePurchaseCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, 
    ICurrentUserInfo currentUserInfo)
    : IRequestHandler<CreatePurchaseCommand, Result<PurchaseDto>>
{
    public async Task<Result<PurchaseDto>> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateRequestModel(request.CreatePurchaseDto);
        if (!validationResult.Succeeded) return validationResult.ConvertTo<PurchaseDto>();
        
        var subscriptionResult = await applicationUnitOfWork.SubscriptionsRepository.GetById(
            request.CreatePurchaseDto.SubscriptionId, cancellationToken);
        if (!subscriptionResult.Succeeded) return subscriptionResult.ConvertTo<PurchaseDto>();
        
        var purchase = Purchase.Create(
            currentUserInfo.Id, validationResult.Data!.SubscriptionId, subscriptionResult.Data!.UsageLimit);
        
        var result = await applicationUnitOfWork.PurchasesRepository.Insert(purchase, 
            currentUserInfo.Id, cancellationToken);
        await applicationUnitOfWork.SaveAsync(cancellationToken);
        
        if (!result.Succeeded) return result.ConvertTo<PurchaseDto>();
        
        return Result.Success(PurchaseDto.Create(result.Data!));
    }
    
    private Result<CreatePurchaseDto> ValidateRequestModel(CreatePurchaseDto createPurchaseDto)
    {
        if (string.IsNullOrWhiteSpace(createPurchaseDto.SubscriptionId))
            return Result.BadRequest<CreatePurchaseDto>("SubscriptionId is required.");
        
        return Result.Success(createPurchaseDto);
    }
}
