using Application.Core.Services.DateTimeProvider;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Purchases;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Repositories.Generic;

namespace ThiIsFine.Infrastructure.Repositories;

public sealed class PurchasesRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    : FullAuditedRepository<Purchase>(dbContext, dateTimeProvider), IPurchasesRepository;
