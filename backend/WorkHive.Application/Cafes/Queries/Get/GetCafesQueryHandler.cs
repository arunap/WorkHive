using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Cafes.Queries.Dtos;

namespace WorkHive.Application.Cafes.Queries.Get
{
    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, List<CafesByLocationResult>>
    {

        private readonly IApplicationDbContext _context;

        public GetCafesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CafesByLocationResult>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var query = from c in _context.Cafes
                        join e in _context.Employees on c.Id equals e.CafeId into employeeGroup
                        from e in employeeGroup.DefaultIfEmpty() // Left join for Employees
                        join f in _context.FileStores on c.LogoId equals f.Id into fileGroup
                        from f in fileGroup.DefaultIfEmpty() // Left join for FileStores
                        group new { c, f, e } by new { c.Id, c.Name, c.Description, c.Location, LogoPath = f != null ? f.FilePath : null } into grouped
                        select new CafesByLocationResult
                        {
                            CafeId = grouped.Key.Id,
                            Name = grouped.Key.Name,
                            Description = grouped.Key.Description,
                            Location = grouped.Key.Location,
                            LogoPath = grouped.Key.LogoPath,
                            EmployeeCount = grouped.Count(x => x.e != null) // Count only non-null employees
                        };

            return await query.OrderByDescending(o => o.EmployeeCount).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}