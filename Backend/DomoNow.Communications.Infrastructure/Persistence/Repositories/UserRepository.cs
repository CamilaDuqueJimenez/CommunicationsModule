using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Repositories
{
    internal class UserRepository(DataBaseContext context, IUnitOfWork unitOfWork) : IUserRepository
    {
        private readonly DataBaseContext _Context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IUnitOfWork _UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task Create(User user)
        {
            try
            {
                await _UnitOfWork.BeginTransactionAsync();
                await _Context.Users.AddAsync(user);
                await _UnitOfWork.CommitTransactionAsync();
                await _UnitOfWork.SaveChangesAsync();
            }
            catch
            {
                await _UnitOfWork.RollbackTransactionAsync();
            }
        }

        public async Task DeleteById(Guid Id)
        {
            try
            {
                await _UnitOfWork.BeginTransactionAsync();
                await _Context.Users.Where(u => u.Id == Id).ExecuteDeleteAsync();
                await _UnitOfWork.CommitTransactionAsync();
                await _UnitOfWork.SaveChangesAsync();
            }
            catch
            {
                await _UnitOfWork.RollbackTransactionAsync();
            }
        }

        public async Task<IReadOnlyList<User>> GetAll() => await _Context.Users.ToListAsync();

        public async Task<User?> GetById(Guid Id) => await _Context.Users.Where(u => u.Id == Id).SingleOrDefaultAsync();

        public async Task Update(User user)
        {
            try
            {
                await _UnitOfWork.BeginTransactionAsync();
                await _Context.Users.Where(u => u.Id == user.Id).ExecuteUpdateAsync(u => u.SetProperty(p => p.LastName, user.LastName)
                .SetProperty(p => p.Name, user.Name).SetProperty(p => p.Email, user.Email));
                await _UnitOfWork.CommitTransactionAsync();
                await _UnitOfWork.SaveChangesAsync();
            }
            catch
            {
                await _UnitOfWork.RollbackTransactionAsync();
            }
        }
    }
}
