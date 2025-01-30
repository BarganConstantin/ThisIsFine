using ThiIsFine.Api.Models.Base;
using ThiIsFine.Application.Users.Queries.GetAll.DTOs;

namespace ThiIsFine.Api.Models.Users.Out;

public record GetAllModel : IResponseModel<IEnumerable<BriefUserDto>>
{
    public IEnumerable<BriefUserModel>? Users { get; set; }
    
    public object? Convert(IEnumerable<BriefUserDto>? dto)
    {
        if (dto == null) return default;
        
        Users = dto.Select(x => new BriefUserModel(x.Id, x.Email));
        
        return this;
    }
}

public record BriefUserModel(
    string? Id,
    string? Email);
