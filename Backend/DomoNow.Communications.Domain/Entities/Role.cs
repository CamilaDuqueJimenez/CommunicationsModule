namespace DomoNow.Communications.Domain.Entities
{
    public partial class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public ICollection<User> Users { get; private set; } = new List<User>();
    }
}
