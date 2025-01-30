using Application.Core.Responses;
using Application.Core.Services.Identity;
using ThiIsFine.Application.Users.Queries.GetById.DTOs;

namespace ThiIsFine.Application.Users.Queries.GetById;

public class GetUserByIdQueryHandler(
    IIdentityService identityService)
    : IRequestHandler<GetUserByIdQuery, Result<UserDetailsDto>>
{
    public async Task<Result<UserDetailsDto>> Handle(
        GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await identityService.GetUserById(request.Id, cancellationToken);
        return !userResult.Succeeded 
            ? userResult.ConvertTo<UserDetailsDto>() 
            : Result.Success(UserDetailsDto.Create(userResult.Data!));
    }
}
