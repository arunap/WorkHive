using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Employees;
using WorkHive.Domain.FileInfo;
using WorkHive.Domain.Shared;

namespace WorkHive.Infrastructure.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, IDateTimeProvider dateTimeProvider) : DbContext(options), IApplicationDbContext
    {
        private readonly IMediator _mediator = mediator;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public DbSet<Cafe> Cafes => Set<Cafe>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<FileStore> FileStores => Set<FileStore>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var contextUser = $"{Environment.UserDomainName}\\{Environment.UserName}";

            //TODO: can move to a interceptor
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.CreatedBy = contextUser; //TODO: This will be replaced Identity Server
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.LastModifiedBy = contextUser; //TODO: This will be replaced Identity Server
                        break;

                    case EntityState.Deleted:
                        // Mark the entity as Modified instead of Deleted
                        entry.State = EntityState.Modified;

                        entry.Entity.IsDeleted = true;
                        entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.LastModifiedBy = contextUser; //TODO: This will be replaced Identity Server

                        break;
                }
            }


            var result = await base.SaveChangesAsync(cancellationToken);

            // publish domain events
            await _mediator.DispatchDomainEvents(this);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}