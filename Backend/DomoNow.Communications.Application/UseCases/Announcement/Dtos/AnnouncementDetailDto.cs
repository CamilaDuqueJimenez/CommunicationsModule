using DomoNow.Communications.Domain.Enums;

namespace DomoNow.Communications.Application.UseCases.Announcement.Dtos
{
    public class AnnouncementDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public TargetScope TargetScope { get; set; }
        public Guid? TowerId { get; set; }
        public Guid? ApartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool HasRead { get; set; }
    }
}
