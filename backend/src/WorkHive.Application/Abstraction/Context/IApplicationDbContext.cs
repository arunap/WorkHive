using Microsoft.EntityFrameworkCore;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Employees;
using WorkHive.Domain.FileInfo;

namespace WorkHive.Application.Abstraction.Context
{
    public interface IApplicationDbContext
    {
        public DbSet<Cafe> Cafes { get; }
        public DbSet<Employee> Employees { get; }
        public DbSet<FileStore> FileStores { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}