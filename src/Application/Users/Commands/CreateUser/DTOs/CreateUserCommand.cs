using Application.Core.Responses;
using Application.Core.Services.Identity.DTOs;

namespace ThiIsFine.Application.Users.Commands.CreateUser.DTOs;

public record CreateUserCommand(RegisterUserDto RegisterUserDto) : IRequest<Result<BriefUserDto>>;
