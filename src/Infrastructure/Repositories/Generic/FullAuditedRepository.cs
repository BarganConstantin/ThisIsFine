using Application.Core.Repositories.Generic;
using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.DateTimeProvider;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ThiIsFine.Infrastructure.Data;

namespace ThiIsFine.Infrastructure.Repositories.Generic
{
    public abstract class FullAuditedRepository<T> : AuditedRepository<T>, IFullAuditedRepository<T> where T : EntityFullAudited
    {
        protected FullAuditedRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
        {
        }

        public virtual async Task<Result<T>> Delete(T obj, string userId, CancellationToken cancellationToken)
        {
            obj.IsDeleted = true;
            obj.DeleterUserId = userId;
            obj.DeletionTime = _dateTimeProvider.UtcNow;

            return await Task.FromResult(new Result<T>() { Data = obj, ResultStatus = ResultStatus.Success });
        }

        public override async Task<Result<T>> GetById(string id, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
            if (data == null)
            {
                return new Result<T>()
                {
                    ResultStatus = ResultStatus.NotFound,
                    Message = $"{typeof(T).Name} with id {id} was not found"
                };
            }

            return new Result<T>() { Data = data, ResultStatus = ResultStatus.Success };
        }

        public override Task<IQueryable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_dbContext.Set<T>().Where(e => !e.IsDeleted));
        }
    }
}
