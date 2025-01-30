namespace Application.Core.Repositories.UnitsOfWork.Base
{
    public interface IUnitOfWork
    {
        Task CreateTransaction(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task Rollback(CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
        bool HasActiveTransaction();
    }
}
