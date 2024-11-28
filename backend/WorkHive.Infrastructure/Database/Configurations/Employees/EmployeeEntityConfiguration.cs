using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkHive.Domain.Employees;

namespace WorkHive.Infrastructure.Database.Configurations.Employees
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            // Create a unique index on Email
            builder.HasIndex(e => e.EmailAddress)
                .IsUnique();

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(8);

            builder.Property(e => e.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.StartedAt)
                .HasConversion<DateTime>()
                .IsRequired();

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();

            builder
                .HasOne(e => e.Cafe)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.CafeId)
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

            builder.HasQueryFilter(e => e.IsDeleted != true);
        }
    }
}