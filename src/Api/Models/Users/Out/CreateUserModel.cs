using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Users.Commands.CreateUser.DTOs;

namespace ThiIsFine.Api.Models.Users.Out;

public class CreateUserModel : IResponseModel<BriefUserDto>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    
    public object? Convert(BriefUserDto? dto)
    {
        if (dto == null) return default;
        
        Id = dto.Id;
        UserName = dto.UserName;
        Email = dto.Email;
        
        return this;
    }
}
