using Application.Core.Responses;
using ThiIsFine.Application.Purchases.Base;

namespace ThiIsFine.Application.Purchases.Queries.GetAll.DTOs;

public sealed record GetAllPurchasesQuery(string? UserId) : IRequest<Result<IEnumerable<PurchaseDto>>>;
