using DomoNow.Communications.Application.Commons;
using DomoNow.Communications.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DomoNow.Communications.Infrastructure.Persistence.Services
{
    public class UnitOfWork<TContext>(TContext context) : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext? _context = context ?? throw new ArgumentNullException(nameof(context));
        private IDbContextTransaction? _transaction;
        public async Task BeginTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException(GeneralConstants.EXISTING_TRANSACTION);
            }
            _transaction = await _context!.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task CommitTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException(GeneralConstants.NON_EXISTENT_TRANSACTION);
            }
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransaction(cancellationToken);
                throw;
            }
            finally
            {
                await DisposeAsync();
            }
        }
        public async Task RollbackTransaction(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                return;
            }
            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await DisposeAsync();
            }
        }
        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
