using Application.Core.Repositories.Generic;
using ThiIsFine.Domain.Entities.Subscriptions;

namespace ThiIsFine.Application.Repositories;

public interface ISubscriptionsRepository : IFullAuditedRepository<Subscription>;
