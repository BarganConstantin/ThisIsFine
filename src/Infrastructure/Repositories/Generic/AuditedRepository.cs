using Application.Core.Repositories.Generic;
using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.DateTimeProvider;
using Domain.Core.Entities;
using ThiIsFine.Infrastructure.Data;

namespace ThiIsFine.Infrastructure.Repositories.Generic
{
    public abstract class AuditedRepository<T> : DefaultRepository<T>, IAuditedRepository<T> where T : EntityAudited
    {
        protected AuditedRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
        {
        }

        public virtual async Task<Result<T>> Update(T obj, string userId, CancellationToken cancellationToken)
        {
            obj.LastModifierUserId = userId;
            obj.LastModificationTime = _dateTimeProvider.UtcNow;

            _dbContext.Set<T>().Update(obj);

            return await Task.FromResult(new Result<T>() { Data = obj, ResultStatus = ResultStatus.Success});
        }
    }
}
