using ThiIsFine.Application.Users.Queries.GetById.DTOs;

namespace ThiIsFine.Api.Models.Users.In;

public record GetCurrentUserModel
{
    private string? UserId { get; set; }
    
    public GetCurrentUserModel SetUserId(string? userId)
    {
        UserId = userId;
        return this;
    }
    
    public GetUserByIdQuery Convert()
    {
        return new GetUserByIdQuery(UserId);
    }
}
