namespace DomoNow.Communications.Application.UseCases.Announcement.Dtos
{
    public class AnnouncementReaderDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool HasRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
