using Application.Core.Responses;
using Domain.Core.Entities;

namespace Application.Core.Repositories.Generic
{
    public interface IFullAuditedRepository<T> : IAuditedRepository<T> where T : EntityFullAudited
    { 
        Task<Result<T>> Delete(T obj, string userId, CancellationToken cancellationToken = default);
    }
}
