using Application.Core.Responses;
using Application.Core.Services.Identity;
using ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

namespace ThiIsFine.Application.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler(
    IIdentityService identityService) 
    : IRequestHandler<AuthenticateUserCommand, Result<UserTokensDto>>
{
    public async Task<Result<UserTokensDto>> Handle(
        AuthenticateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await identityService.AuthenticateUser(
            request.AuthenticateUserDto.Username, 
            request.AuthenticateUserDto.Password,
            cancellationToken);

        return new Result<UserTokensDto>()
        {
            Data = UserTokensDto.Create(result.Data), 
            Message = result.Message, 
            ResultStatus = result.ResultStatus,
        };
    }
}
