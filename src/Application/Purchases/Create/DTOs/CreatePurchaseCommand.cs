using Application.Core.Responses;
using ThiIsFine.Application.Purchases.Base;

namespace ThiIsFine.Application.Purchases.Create.DTOs;

public sealed record CreatePurchaseCommand(CreatePurchaseDto CreatePurchaseDto) : IRequest<Result<PurchaseDto>>;
