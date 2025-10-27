using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomoNow.Communications.Infrastructure.Persistence.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> context) : DbContext(context)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
        }
    }
}
