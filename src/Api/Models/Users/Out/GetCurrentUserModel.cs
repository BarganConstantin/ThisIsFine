using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Users.Queries.GetById.DTOs;

namespace ThiIsFine.Api.Models.Users.Out;

public record GetCurrentUserModel : IResponseModel<UserDetailsDto>
{
    public string? Id { get; private set; }
    public string? UserName { get; private set; }
    public string? Email { get; private set; }
    
    public object? Convert(UserDetailsDto? dto)
    {
        if (dto is null) return default;
        
        Id = dto.Id;
        UserName = dto.UserName;
        Email = dto.Email;
        
        return this;
    }
}
