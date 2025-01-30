using Application.Core.Responses;
using Domain.Core.Entities;

namespace Application.Core.Repositories.Generic
{
    public interface IDefaultRepository<T> where T : EntityBaseAudited
    {
        Task<IQueryable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<Result<T>> GetById(string id, CancellationToken cancellationToken = default);
        Task<Result<T>> Insert(T obj, string userId, CancellationToken cancellationToken = default);
        Task<Result<T>> Reload(T obj, CancellationToken cancellationToken = default);
    }
}
