using Microsoft.EntityFrameworkCore;
using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class ReadConfirmationConfiguration : IEntityTypeConfiguration<ReadConfirmation>
    {
        public void Configure(EntityTypeBuilder<ReadConfirmation> builder)
        {
            builder.ToTable("ReadConfirmations");

            builder.HasKey(x => new { x.AnnouncementId, x.UserId });

            builder.HasOne(x => x.Announcement)
                   .WithMany(a => a.ReadConfirmations)
                   .HasForeignKey(x => x.AnnouncementId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.ReadConfirmations)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
