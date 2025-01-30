using Application.Core.Responses;
using Application.Core.Services.Identity;
using ThiIsFine.Application.Users.Queries.GetAll.DTOs;

namespace ThiIsFine.Application.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(
    IIdentityService identityService)
    : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<BriefUserDto>>>
{
    public async Task<Result<IEnumerable<BriefUserDto>>> Handle(
        GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var usersResult = await identityService.GetAllUsers(cancellationToken);
        var briefUsers = usersResult.Data?.Select(u => BriefUserDto.Create(u.Id, u.Email));
        return new Result<IEnumerable<BriefUserDto>>() 
            { Data = briefUsers, ResultStatus = usersResult.ResultStatus };
    }
}
