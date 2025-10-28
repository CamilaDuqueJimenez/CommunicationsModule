using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Repositories
{
    internal class RoleRepository(DataBaseContext context, IUnitOfWork unitOfWork) : IRoleRepository
    {
        private readonly DataBaseContext _context = context;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _unitOfWork.SaveChanges();
        }
        public async Task<IReadOnlyList<Role>> GetAll() => await _context.Roles.AsNoTracking().ToListAsync();
    }
}
