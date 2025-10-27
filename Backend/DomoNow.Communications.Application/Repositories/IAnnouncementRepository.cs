using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IAnnouncementRepository
    {
        Task Create(Announcement entity);
        Task Update(Announcement entity);
        Task<Announcement?> GetById(Guid id);
        IQueryable<Announcement> Query();
    }
}
