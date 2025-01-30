using Application.Core.Responses;
using Application.Core.Services.Identity;
using ThiIsFine.Application.Users.Commands.CreateUser.DTOs;

namespace ThiIsFine.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<CreateUserCommand, Result<BriefUserDto>>
{
    public async Task<Result<BriefUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.CreateUserAsync(request.RegisterUserDto, cancellationToken);
        if (!result.Succeeded) return result.ConvertTo<BriefUserDto>();
        
        return Result.Success(BriefUserDto.Create(result.Data)!);
    }
}
