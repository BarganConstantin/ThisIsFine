using Application.Core.Repositories.Generic;
using ThiIsFine.Domain.Entities.Images;

namespace ThiIsFine.Application.Repositories;

public interface IImageRepository : IFullAuditedRepository<AspNetImage>;
