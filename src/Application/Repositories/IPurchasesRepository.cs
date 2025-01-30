using Application.Core.Repositories.Generic;
using ThiIsFine.Domain.Entities.Purchases;

namespace ThiIsFine.Application.Repositories;

public interface IPurchasesRepository : IFullAuditedRepository<Purchase>;
