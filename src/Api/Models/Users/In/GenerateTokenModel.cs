using ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

namespace ThiIsFine.Api.Models.Users.In;

public record GenerateTokenModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    
    public AuthenticateUserCommand Convert()
    {
        return new AuthenticateUserCommand(new AuthenticateUserDto
        {
            Username = Username,
            Password = Password
        });
    }
}
