using DomoNow.Communications.Domain.Enums;

namespace DomoNow.Communications.Application.UseCases.Announcement.Dtos
{
    public class AnnouncementListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public TargetScope TargetScope { get; set; }
    }
}
