using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Repositories
{
    internal class ReadConfirmationRepository(DataBaseContext context, IUnitOfWork unitOfWork) : IReadConfirmationRepository
    {
        private readonly DataBaseContext _context = context;
        private readonly IUnitOfWork _uow = unitOfWork;

        public async Task<bool> Exists(Guid announcementId, Guid userId) =>
            await _context.ReadConfirmations.AnyAsync(r => r.AnnouncementId == announcementId && r.UserId == userId);

        public async Task Create(ReadConfirmation entity)
        {
            await _context.ReadConfirmations.AddAsync(entity);
            await _uow.SaveChanges();
        }

        public async Task<IReadOnlyList<ReadConfirmation>> GetByAnnouncement(Guid announcementId) =>
            await _context.ReadConfirmations
                .Where(r => r.AnnouncementId == announcementId)
                .AsNoTracking()
                .ToListAsync();
    }
}
