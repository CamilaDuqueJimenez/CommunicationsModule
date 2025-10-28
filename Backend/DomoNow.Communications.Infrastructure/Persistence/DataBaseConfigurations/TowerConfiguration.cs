using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class TowerConfiguration : IEntityTypeConfiguration<Tower>
    {
        public void Configure(EntityTypeBuilder<Tower> builder)
        {
            builder.ToTable("Towers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
