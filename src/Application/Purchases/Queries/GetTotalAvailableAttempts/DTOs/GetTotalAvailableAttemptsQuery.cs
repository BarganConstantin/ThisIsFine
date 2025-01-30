using Application.Core.Responses;

namespace ThiIsFine.Application.Purchases.Queries.GetTotalAvailableAttempts.DTOs;

public sealed record GetTotalAvailableAttemptsQuery : IRequest<Result<int>>;
