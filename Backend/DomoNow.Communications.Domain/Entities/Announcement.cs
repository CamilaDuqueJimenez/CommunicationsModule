using DomoNow.Communications.Domain.Enums;

namespace DomoNow.Communications.Domain.Entities
{
    public partial class Announcement
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public TargetScope TargetScope { get; private set; }
        public Guid? TowerId { get; private set; }
        public Guid? ApartmentId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid? UpdatedBy { get; private set; }

        public Tower? Tower { get; private set; }
        public Apartment? Apartment { get; private set; }
        public User? CreatedByUser { get; private set; }
        public User? UpdatedByUser { get; private set; }
        public ICollection<Attachment> Attachments { get; private set; } = new List<Attachment>();
        public ICollection<ReadConfirmation> ReadConfirmations { get; private set; } = new List<ReadConfirmation>();

        public static Announcement Create(string title, string description, TargetScope targetScope, Guid? towerId, Guid? apartmentId, Guid createdBy)
        {
            return new Announcement
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                TargetScope = targetScope,
                TowerId = targetScope == TargetScope.Tower ? towerId : null,
                ApartmentId = targetScope == TargetScope.Apartment ? apartmentId : null,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                IsActive = true
            };
        }

        public void UpdateTarget(TargetScope scope, Guid? towerId, Guid? apartmentId)
        {
            TargetScope = scope;
            TowerId = scope == TargetScope.Tower ? towerId : null;
            ApartmentId = scope == TargetScope.Apartment ? apartmentId : null;
        }

        public void UpdateContent(string title, string description, Guid updaterId)
        {
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updaterId;
        }
    }
}
