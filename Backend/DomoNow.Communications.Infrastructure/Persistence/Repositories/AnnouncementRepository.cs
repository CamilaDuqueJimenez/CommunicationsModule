using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Repositories
{
    internal class AnnouncementRepository(DataBaseContext context, IUnitOfWork unitOfWork) : IAnnouncementRepository
    {
        private readonly DataBaseContext _context = context;
        private readonly IUnitOfWork _uow = unitOfWork;

        public async Task Create(Announcement entity)
        {
            await _context.Announcements.AddAsync(entity);
            await _uow.SaveChanges();
        }

        public async Task Update(Announcement entity)
        {
            _context.Announcements.Update(entity);
            await _uow.SaveChanges();
        }

        public async Task<Announcement?> GetById(Guid id) =>
            await _context.Announcements
                .Include(x => x.ReadConfirmations)
                .FirstOrDefaultAsync(x => x.Id == id);

        public IQueryable<Announcement> Query() =>
            _context.Announcements.AsNoTracking();
    }
}
