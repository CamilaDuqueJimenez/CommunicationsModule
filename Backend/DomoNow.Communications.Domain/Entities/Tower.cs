namespace DomoNow.Communications.Domain.Entities
{
    public partial class Tower
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Code { get; private set; } = default!;
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public ICollection<Apartment> Apartments { get; private set; } = new List<Apartment>();
        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<Announcement> Announcements { get; private set; } = new List<Announcement>();
    }
}
