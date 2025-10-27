using DomoNow.Communications.Application.Services;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    internal class UnitOfWork : IUnitOfWork
    {
        public Task BeginTransactionAsync(CancellationToken pCancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync(CancellationToken pCancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync(CancellationToken pCancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken pCancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
