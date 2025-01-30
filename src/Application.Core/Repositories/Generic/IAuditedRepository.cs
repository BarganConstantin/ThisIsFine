using Application.Core.Responses;
using Domain.Core.Entities;

namespace Application.Core.Repositories.Generic
{
    public interface IAuditedRepository<T> : IDefaultRepository<T> where T : EntityAudited
    {
        Task<Result<T>> Update(T obj, string userId, CancellationToken cancellationToken = default);
    }
}
