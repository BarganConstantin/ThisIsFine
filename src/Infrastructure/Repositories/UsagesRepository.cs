using Application.Core.Services.DateTimeProvider;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Usages;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Repositories.Generic;

namespace ThiIsFine.Infrastructure.Repositories;

public sealed class UsagesRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    : FullAuditedRepository<Usage>(dbContext, dateTimeProvider), IUsagesRepository;
