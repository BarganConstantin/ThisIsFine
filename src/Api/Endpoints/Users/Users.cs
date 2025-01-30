using Domain.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using ThiIsFine.Api.Infrastructure;
using ThiIsFine.Api.ResponseMapper;
using ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;
using ThiIsFine.Application.Users.Commands.CreateUser.DTOs;
using CreateUserModel = ThiIsFine.Api.Models.Users.In.CreateUserModel;
using GenerateTokenModel = ThiIsFine.Api.Models.Users.In.GenerateTokenModel;
using GetAllModel = ThiIsFine.Api.Models.Users.Out.GetAllModel;
using In_GetAllModel = ThiIsFine.Api.Models.Users.In.GetAllModel;
using Out_CreateUserModel = ThiIsFine.Api.Models.Users.Out.CreateUserModel;
using Out_GenerateTokenModel = ThiIsFine.Api.Models.Users.Out.GenerateTokenModel;

namespace ThiIsFine.Api.Endpoints.Users;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateUser, "register")
            .MapPost(GenerateToken, "login")
            .MapPost(GetAllUsers, "users");
    }

    /// <summary>
    /// Create a user
    /// </summary>
    private static async Task<IResult> CreateUser(
        ISender sender, 
        IResponseMapper responseMapper,
        CreateUserModel request)
    {
        return responseMapper.ExecuteAndMapStatus<Out_CreateUserModel, BriefUserDto>(
            await sender.Send(request.Convert()));
    }

    /// <summary>
    /// Generate a token
    /// </summary>
    private static async Task<IResult> GenerateToken(
        ISender sender, 
        IResponseMapper responseMapper,
        GenerateTokenModel request)
    {
        return responseMapper.ExecuteAndMapStatus<Out_GenerateTokenModel, UserTokensDto>(
            await sender.Send(request.Convert()));
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    [Authorize(Policy = AccessPolicy.UserAccessPolicy)]
    private static async Task<IResult> GetAllUsers(
        ISender sender, 
        IResponseMapper responseMapper)
    {
        return responseMapper.ExecuteAndMapStatus<GetAllModel, 
            IEnumerable<Application.Users.Queries.GetAll.DTOs.BriefUserDto>>(
            await sender.Send(new In_GetAllModel().Convert()));
    }
}
