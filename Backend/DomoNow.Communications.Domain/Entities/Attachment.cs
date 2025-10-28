namespace DomoNow.Communications.Domain.Entities
{
    public partial class Attachment
    {
        public Guid Id { get; private set; }
        public Guid AnnouncementId { get; private set; }
        public string FileName { get; private set; } = default!;
        public string ContentType { get; private set; } = default!;
        public string FileUrl { get; private set; } = default!;
        public long SizeBytes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Announcement? Announcement { get; private set; }
    }
}
