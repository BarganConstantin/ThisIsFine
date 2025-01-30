using Application.Core.Services.Identity.DTOs;
using ThiIsFine.Application.Users.Commands.CreateUser.DTOs;

namespace ThiIsFine.Api.Models.Users.In;

public record CreateUserModel
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public CreateUserCommand Convert()
    {
        return new CreateUserCommand(new RegisterUserDto
        {
            UserName = UserName,
            Email = Email,
            Password = Password,
        });
    }
}
