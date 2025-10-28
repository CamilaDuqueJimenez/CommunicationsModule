namespace DomoNow.Communications.Domain.Entities
{
    public partial class Apartment
    {
        public Guid Id { get; private set; }
        public Guid TowerId { get; private set; }
        public string Number { get; private set; } = default!;
        public int? Floor { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public Tower? Tower { get; private set; }
        public ICollection<User> Users { get; private set; } = new List<User>();
        public ICollection<Announcement> Announcements { get; private set; } = new List<Announcement>();
    }
}
