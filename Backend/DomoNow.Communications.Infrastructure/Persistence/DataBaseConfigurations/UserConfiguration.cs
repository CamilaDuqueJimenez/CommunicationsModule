using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.DocumentNumber)
                .HasMaxLength(50);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.DocumentNumber)
                .IsUnique();

            builder.HasOne(x => x.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Tower)
                   .WithMany(t => t.Users)
                   .HasForeignKey(x => x.TowerId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Apartment)
                   .WithMany(a => a.Users)
                   .HasForeignKey(x => x.ApartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
