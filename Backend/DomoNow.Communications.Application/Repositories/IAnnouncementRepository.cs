using DomoNow.Communications.Application.Models;
using DomoNow.Communications.Application.UseCases.Announcement.Dtos;
using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IAnnouncementRepository
    {
        Task Create(Announcement entity);
        Task Update(Announcement entity);
        Task<Announcement?> GetById(Guid id);
        Task<PaginationResponse<AnnouncementListItemDto>> GetPagedForUser(Guid? towerId, Guid? apartmentId, QueryParam filter, bool onlyActive = true);
    }
}
