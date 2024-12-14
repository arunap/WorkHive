using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Employees;
using WorkHive.Domain.FileInfo;
using WorkHive.Domain.Shared;

namespace WorkHive.Infrastructure.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : DbContext(options), IApplicationDbContext
    {
        private readonly IMediator _mediator = mediator;
        private IDbContextTransaction? _currentTransaction;

        public DbSet<Cafe> Cafes => Set<Cafe>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<FileStore> FileStores => Set<FileStore>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            // publish domain events
            await _mediator.DispatchDomainEvents(this);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        // Begin a new transaction
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) throw new InvalidOperationException("Transaction already started.");

            _currentTransaction = await Database.BeginTransactionAsync();
        }

        // Commit the current transaction
        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null) throw new InvalidOperationException("No transaction started.");

            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        // Rollback the current transaction
        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null) throw new InvalidOperationException("No transaction started.");

            await _currentTransaction.RollbackAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

    }
}