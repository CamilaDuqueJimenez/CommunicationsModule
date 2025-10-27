using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.ToTable("Announcements");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.TargetScope)
                   .HasConversion<byte>()
                   .IsRequired();

            builder.HasOne(x => x.Tower)
                   .WithMany(t => t.Announcements)
                   .HasForeignKey(x => x.TowerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Apartment)
                   .WithMany(a => a.Announcements)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.CreatedByUser)
                   .WithMany(u => u.AnnouncementsCreated)
                   .HasForeignKey(x => x.CreatedBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UpdatedByUser)
                   .WithMany(u => u.AnnouncementsUpdated)
                   .HasForeignKey(x => x.UpdatedBy)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
