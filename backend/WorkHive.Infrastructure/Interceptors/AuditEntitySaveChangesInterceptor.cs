using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Shared;

namespace WorkHive.Infrastructure.Interceptors
{
    public class AuditEntitySaveChangesInterceptor(IDateTimeProvider dateTimeProvider, IUserContext userContext) : SaveChangesInterceptor
    {
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
        private readonly IUserContext _userContext = userContext;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null) UpdateAuditableProperties(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null) UpdateAuditableProperties(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditableProperties(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.CreatedBy = _userContext.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.LastModifiedBy = _userContext.UserId;
                        break;

                    case EntityState.Deleted:
                        // Mark the entity as Modified instead of Deleted
                        entry.State = EntityState.Modified;

                        entry.Entity.IsDeleted = true;
                        entry.Entity.LastModifiedDate = _dateTimeProvider.UtcNow;
                        entry.Entity.LastModifiedBy = _userContext.UserId;

                        break;
                }
            }
        }
    }
}