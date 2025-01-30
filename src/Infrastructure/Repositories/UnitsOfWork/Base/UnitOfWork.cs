using Application.Core.Repositories.UnitsOfWork.Base;
using Microsoft.EntityFrameworkCore.Storage;
using ThiIsFine.Infrastructure.Data;

namespace ThiIsFine.Infrastructure.Repositories.UnitsOfWork.Base
{
    public abstract class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction? _objTran;

        protected UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await SaveAsync(cancellationToken);
            if (_objTran != null) 
                await _objTran.CommitAsync(cancellationToken);
        }

        public Task CreateTransaction(CancellationToken cancellationToken)
        {
            _objTran = _dbContext.Database.BeginTransaction();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task Rollback(CancellationToken cancellationToken)
        {
            _objTran?.Rollback();
            _objTran?.Dispose();

            return Task.CompletedTask;
        }

        public async Task SaveAsync(CancellationToken cancellationToken) =>
            await _dbContext.SaveChangesAsync(cancellationToken);
        
        public bool HasActiveTransaction() => _objTran != null;
    }
}
