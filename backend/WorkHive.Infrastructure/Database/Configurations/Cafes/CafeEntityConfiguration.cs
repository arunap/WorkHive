using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHive.Domain.Cafes;

namespace WorkHive.Infrastructure.Database.Configurations.Cafes
{
    public class CafeEntityConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            // Foreign key
            builder.HasOne(c => c.Logo)
                .WithMany(fs => fs.Cafes)
                .HasForeignKey(c => c.LogoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();

            builder.Property(e => e.CreatedBy)
           .HasMaxLength(100)
           .IsRequired(false);

            builder.Property(e => e.CreatedDate)
                .IsRequired(false);

            builder.Property(e => e.LastModifiedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(e => e.LastModifiedDate)
                .IsRequired(false);

            builder.HasQueryFilter(e => e.IsDeleted != true);
        }
    }
}