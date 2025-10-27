using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Apartments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasMaxLength(20);

            builder.HasIndex(x => new { x.TowerId, x.Number }).IsUnique();

            builder.HasOne(x => x.Tower)
                   .WithMany(t => t.Apartments)
                   .HasForeignKey(x => x.TowerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
