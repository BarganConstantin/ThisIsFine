namespace ThiIsFine.Application.Users.Commands.AuthenticateUser.DTOs;

public class AuthenticateUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
