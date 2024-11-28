using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHive.Domain.FileInfo;

namespace WorkHive.Infrastructure.Database.Configurations.FileInfo
{
    public class FileInfoConfiguration : IEntityTypeConfiguration<FileStore>
    {
        public void Configure(EntityTypeBuilder<FileStore> builder)
        {
            builder.HasKey(f => f.Id);  // Primary key

            builder.Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(f => f.FileSize)
                .IsRequired();

            builder.Property(f => f.ContentType)
                .HasMaxLength(100);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();

            // One-to-many relationships
            builder.HasMany(fs => fs.Cafes)
                .WithOne(c => c.Logo)
                .HasForeignKey(c => c.LogoId)
                .OnDelete(DeleteBehavior.NoAction);

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
        }
    }
}