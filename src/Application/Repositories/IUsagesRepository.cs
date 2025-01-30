using Application.Core.Repositories.Generic;
using ThiIsFine.Domain.Entities.Usages;

namespace ThiIsFine.Application.Repositories;

public interface IUsagesRepository : IFullAuditedRepository<Usage>;
