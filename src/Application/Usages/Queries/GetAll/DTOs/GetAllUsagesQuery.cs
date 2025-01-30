using Application.Core.Responses;
using ThiIsFine.Application.Subscriptions.Base.DTOs;
using ThiIsFine.Application.Usages.Base;

namespace ThiIsFine.Application.Usages.Queries.GetAll.DTOs;

public sealed record GetAllUsagesQuery(string UserId) : IRequest<Result<IEnumerable<UsageDto>>>;
