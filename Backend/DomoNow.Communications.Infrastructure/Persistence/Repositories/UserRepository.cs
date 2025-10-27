using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Repositories
{
    internal class UserRepository(DataBaseContext context, IUnitOfWork unitOfWork) : IAuthRepository
    {
        private readonly DataBaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteById(Guid Id)
        {
            await _context.Users.Where(u => u.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<IReadOnlyList<User>> GetAll() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetByEmail(string email) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetById(Guid Id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Id);

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _unitOfWork.SaveChanges();
        }
    }
}
