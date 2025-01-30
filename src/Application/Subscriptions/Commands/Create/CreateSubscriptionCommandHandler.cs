using Application.Core.Responses;
using Application.Core.Services.UserInfo;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using ThiIsFine.Application.Subscriptions.Commands.Create.DTOs;
using ThiIsFine.Domain.Entities.Subscriptions;

namespace ThiIsFine.Application.Subscriptions.Commands.Create;

public class CreateSubscriptionCommandHandler(IApplicationUnitOfWork applicationUnitOfWork, 
    ICurrentUserInfo currentUserInfo)
    : IRequestHandler<CreateSubscriptionCommand, Result<SubscriptionDto>>
{
    public async Task<Result<SubscriptionDto>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionResult = ValidateAndCreateSubscriptionEntity(request.CreateSubscriptionDto);
        if (!subscriptionResult.Succeeded) return subscriptionResult.ConvertTo<SubscriptionDto>();
        
        var result = await applicationUnitOfWork.SubscriptionsRepository.Insert(
            subscriptionResult.Data!, currentUserInfo.Id, cancellationToken);
        await applicationUnitOfWork.SaveAsync(cancellationToken);
        
        if (!result.Succeeded) return result.ConvertTo<SubscriptionDto>();
        
        return Result.Success(SubscriptionDto.Create(result.Data!));
    }
    
    private Result<Subscription> ValidateAndCreateSubscriptionEntity(CreateSubscriptionDto createSubscriptionDto)
    {
        if (createSubscriptionDto.Price <= 0) 
            return Result.BadRequest<Subscription>("Price must be greater than 0");
        
        if (createSubscriptionDto.UsageLimit < 0) 
            return Result.BadRequest<Subscription>("Usage limit must be greater than or equal to 0");
        
        if (createSubscriptionDto.Name.Length < 3) 
            return Result.BadRequest<Subscription>("Name must be at least 3 characters long");
        
        if (createSubscriptionDto.Description.Length < 10) 
            return Result.BadRequest<Subscription>("Description must be at least 10 characters long");
        
        var subscription = Subscription.Create(
            name: createSubscriptionDto.Name,
            price: createSubscriptionDto.Price,
            usageLimit: createSubscriptionDto.UsageLimit,
            description: createSubscriptionDto.Description);

        return Result.Success(subscription);
    }
}
