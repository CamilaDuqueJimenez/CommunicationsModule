using DomoNow.Communications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomoNow.Communications.Infrastructure.Persistence.DataBaseConfigurations
{
    internal class ExampleConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(s => s.Id).HasDatabaseName("idx_example_id");

            builder.ToTable("example");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd().HasDefaultValueSql("gen_random_uuid()").IsRequired();

            builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(50).IsRequired();

            builder.Property(s => s.LastName).HasColumnName("last_name").HasMaxLength(50).IsRequired();

            builder.Property(s => s.Email).HasColumnName("email").HasMaxLength(60).IsRequired();
        }
    }
}
