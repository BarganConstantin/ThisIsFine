using ThiIsFine.Application.Users.Queries.GetAll.DTOs;

namespace ThiIsFine.Api.Models.Users.In;

public record GetAllModel
{
    public GetAllUsersQuery Convert()
    {
        return new GetAllUsersQuery();
    }
}
