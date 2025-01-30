using Application.Core.Services.DateTimeProvider;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Domain.Entities.Images;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Repositories.Generic;

namespace ThiIsFine.Infrastructure.Repositories;

public sealed class ImageRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    : FullAuditedRepository<AspNetImage>(dbContext, dateTimeProvider), IImageRepository;
