using ThiIsFine.Domain.Entities.User;

namespace ThiIsFine.Application.Users.Queries.GetById.DTOs;

public record UserDetailsDto
{
    public string? Id { get; private set; }
    public string? UserName { get; private set; }
    public string? Email { get; private set; }
    
    public static UserDetailsDto Create(IThisIsFineUser entity)
    {
        return new UserDetailsDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
        };
    }
}
