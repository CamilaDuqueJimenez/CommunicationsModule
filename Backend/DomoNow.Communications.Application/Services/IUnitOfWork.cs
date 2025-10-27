namespace DomoNow.Communications.Application.Services
{
    public interface IUnitOfWork : IDisposable
    {

        Task BeginTransactionAsync(CancellationToken pCancellationToken = default);
        Task CommitTransactionAsync(CancellationToken pCancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken pCancellationToken = default);
        Task SaveChangesAsync(CancellationToken pCancellationToken = default);
    }
}
