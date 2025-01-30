namespace Application.Core.Services.Identity.DTOs;

public class RegisterUserDto
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
