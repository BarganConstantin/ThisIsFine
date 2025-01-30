using Domain.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using ThiIsFine.Api.Infrastructure;
using ThiIsFine.Api.ResponseMapper;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using CreateSubscriptionModel = ThiIsFine.Api.Models.Subscriptions.In.CreateSubscriptionModel;
using DeleteSubscriptionByIdModel = ThiIsFine.Api.Models.Subscriptions.Out.DeleteSubscriptionByIdModel;
using GetAllSubscriptionModel = ThiIsFine.Api.Models.Subscriptions.Out.GetAllSubscriptionModel;
using GetSubscriptionByIdModel = ThiIsFine.Api.Models.Subscriptions.Out.GetSubscriptionByIdModel;
using In_DeleteSubscriptionByIdModel = ThiIsFine.Api.Models.Subscriptions.In.DeleteSubscriptionByIdModel;
using In_GetAllSubscriptionModel = ThiIsFine.Api.Models.Subscriptions.In.GetAllSubscriptionModel;
using In_GetSubscriptionByIdModel = ThiIsFine.Api.Models.Subscriptions.In.GetSubscriptionByIdModel;
using Out_CreateSubscriptionModel = ThiIsFine.Api.Models.Subscriptions.Out.CreateSubscriptionModel;

namespace ThiIsFine.Api.Endpoints.Subscriptions;

public sealed class Subscriptions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateSubscription)
            .MapGet(GetAllSubscription)
            .MapGet(GetSubscriptionById, "{id}")
            .MapDelete(DeleteSubscriptionById, "{id}");
    }

    /// <summary>
    /// Create a subscription
    /// </summary>
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    private static async Task<IResult> CreateSubscription(ISender sender, IResponseMapper responseMapper,
        CreateSubscriptionModel request)
    {
        return responseMapper.ExecuteAndMapStatus<Out_CreateSubscriptionModel, SubscriptionDto>(
            await sender.Send(request.Convert()));
    }
    
    /// <summary>
    /// Get all subscriptions
    /// </summary>
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    private static async Task<IResult> GetAllSubscription(ISender sender, IResponseMapper responseMapper)
    {
        return responseMapper.ExecuteAndMapStatus<GetAllSubscriptionModel, IEnumerable<SubscriptionDto>>(
            await sender.Send(new In_GetAllSubscriptionModel().Convert()));
    }
    
    /// <summary>
    /// Get subscription by id
    /// </summary>
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    private static async Task<IResult> GetSubscriptionById(ISender sender, IResponseMapper responseMapper, string id)
    {
        return responseMapper.ExecuteAndMapStatus<GetSubscriptionByIdModel, SubscriptionDto>(
            await sender.Send(new In_GetSubscriptionByIdModel(id).Convert()));
    }
    
    /// <summary>
    /// Delete subscription by id
    /// </summary>
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    private static async Task<IResult> DeleteSubscriptionById(ISender sender, IResponseMapper responseMapper, string id)
    {
        return responseMapper.ExecuteAndMapStatus<DeleteSubscriptionByIdModel, SubscriptionDto>(
            await sender.Send(new In_DeleteSubscriptionByIdModel(id).Convert()));
    }
}
