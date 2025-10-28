using Microsoft.EntityFrameworkCore;
using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);

            builder.Property(x => x.ContentType).IsRequired().HasMaxLength(100);

            builder.Property(x => x.FileUrl).IsRequired().HasMaxLength(500);

            builder.HasOne(x => x.Announcement)
                   .WithMany(a => a.Attachments)
                   .HasForeignKey(x => x.AnnouncementId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
