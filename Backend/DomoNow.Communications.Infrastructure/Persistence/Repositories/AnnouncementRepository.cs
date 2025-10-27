using DomoNow.Communications.Application.Models;
using DomoNow.Communications.Application.Repositories;
using DomoNow.Communications.Application.Services;
using DomoNow.Communications.Application.UseCases.Announcement.Dtos;
using DomoNow.Communications.Domain.Entities;
using DomoNow.Communications.Domain.Enums;
using DomoNow.Communications.Infrastructure.Persistence.Context;
using DomoNow.Communications.Infrastructure.Persistence.Services;
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

        public async Task<PaginationResponse<AnnouncementListItemDto>> GetPagedForUser(Guid? towerId, Guid? apartmentId, QueryParam filter, bool onlyActive = true)
        {
            IQueryable<Announcement> query = _context.Announcements.AsNoTracking();

            if (onlyActive)
                query = query.Where(a => a.IsActive);

            if (towerId.HasValue && apartmentId.HasValue)
            {
                query = query.Where(a =>
                    a.TargetScope == TargetScope.All ||
                    (a.TargetScope == TargetScope.Tower && a.TowerId == towerId) ||
                    (a.TargetScope == TargetScope.Apartment && a.ApartmentId == apartmentId));
            }

            query = query.ApplyFiltering(filter, a => a.Title, a => a.Description);

            var projected = query.Select(a => new AnnouncementListItemDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                IsActive = a.IsActive,
                CreatedAt = a.CreatedAt,
                TargetScope = a.TargetScope
            });

            return await PaginationResponse<AnnouncementListItemDto>.Create(projected, filter.PageNumber, filter.PageSize);
        }
    }
}
