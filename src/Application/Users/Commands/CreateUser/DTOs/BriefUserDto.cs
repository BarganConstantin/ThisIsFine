using ThiIsFine.Domain.Entities.User;

namespace ThiIsFine.Application.Users.Commands.CreateUser.DTOs;

public class BriefUserDto
{
    public string? Id { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    
    public static BriefUserDto? Create(IThisIsFineUser? user)
    {
        if (user == null) return default;
        
        return new BriefUserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
