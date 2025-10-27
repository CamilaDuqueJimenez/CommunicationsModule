using DomoNow.Communications.Domain.Entities;

namespace DomoNow.Communications.Application.Repositories
{
    public interface IReadConfirmationRepository
    {
        Task<bool> Exists(Guid announcementId, Guid userId);
        Task Create(ReadConfirmation entity);
        Task<IReadOnlyList<ReadConfirmation>> GetByAnnouncement(Guid announcementId);
    }
}
