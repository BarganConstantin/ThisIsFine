using Application.Core.Responses;

namespace ThiIsFine.Application.Users.Queries.GetAll.DTOs;

public record GetAllUsersQuery : IRequest<Result<IEnumerable<BriefUserDto>>>;
