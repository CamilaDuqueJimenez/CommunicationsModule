using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> context) : DbContext(context)
    {
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Tower> Towers => Set<Tower>();
        public DbSet<Apartment> Apartments => Set<Apartment>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<ReadConfirmation> ReadConfirmations => Set<ReadConfirmation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
