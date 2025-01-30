using ThiIsFine.Application.Base.BaseById;

namespace ThiIsFine.Application.Users.Queries.GetById.DTOs;

public record GetUserByIdQuery : BaseByIdRequest<UserDetailsDto>
{
    public GetUserByIdQuery(string? id) : base(id)
    {
    }
}
