using Application.Core.Responses;

namespace ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

public record AuthenticateUserCommand(AuthenticateUserDto AuthenticateUserDto) : IRequest<Result<UserTokensDto>>;
