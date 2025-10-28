namespace DomoNow.Communications.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; private set; }
        public Guid RoleId { get; private set; }
        public Guid TowerId { get; private set; }
        public Guid ApartmentId { get; private set; }
        public string FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public string? DocumentNumber { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public Role? Role { get; private set; }
        public Tower? Tower { get; private set; }
        public Apartment? Apartment { get; private set; }
        public ICollection<Announcement> AnnouncementsCreated { get; private set; } = new List<Announcement>();
        public ICollection<Announcement> AnnouncementsUpdated { get; private set; } = new List<Announcement>();
        public ICollection<ReadConfirmation> ReadConfirmations { get; private set; } = new List<ReadConfirmation>();
    }
}
