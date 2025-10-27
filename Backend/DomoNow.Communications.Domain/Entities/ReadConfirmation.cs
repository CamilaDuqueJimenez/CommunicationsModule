namespace DomoNow.Communications.Domain.Entities
{
    public partial class ReadConfirmation
    {
        public Guid AnnouncementId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime ReadAt { get; private set; }
        public Announcement? Announcement { get; private set; }
        public User? User { get; private set; }
    }
}
