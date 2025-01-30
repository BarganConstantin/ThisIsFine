using Application.Core.Repositories.Generic;
using Application.Core.Responses;
using Application.Core.Responses.Enum;
using Application.Core.Services.DateTimeProvider;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ThiIsFine.Infrastructure.Data;

namespace ThiIsFine.Infrastructure.Repositories.Generic
{
    public abstract class DefaultRepository<T> : IDefaultRepository<T>, IDisposable where T : EntityBaseAudited
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IDateTimeProvider _dateTimeProvider;

        protected DefaultRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public virtual Task<IQueryable<T>> GetAll(CancellationToken cancellationToken)
        {
            return Task.FromResult(_dbContext.Set<T>().AsQueryable());
        }

        public virtual async Task<Result<T>> GetById(string id, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (data == null)
            {
                return await Task.FromResult(new Result<T>()
                {
                    ResultStatus = ResultStatus.NotFound,
                    Message = $"{typeof(T).Name} with id {id} was not found"
                });
            }

            return await Task.FromResult(new Result<T>() { Data = data, ResultStatus = ResultStatus.Success });
        }

        public virtual async Task<Result<T>> Insert(T obj, string userId, CancellationToken cancellationToken = default)
        {
            obj.CreatorUserId = userId;
            obj.CreationTime = _dateTimeProvider.UtcNow;
            await _dbContext.Set<T>().AddAsync(obj, cancellationToken);

            return await Task.FromResult(new Result<T>() { Data = obj, ResultStatus = ResultStatus.Created });
        }

        public Task<Result<T>> Reload(T obj, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(obj).Reload();
            return Task.FromResult(new Result<T>() { Data = obj, ResultStatus = ResultStatus.Success });
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
