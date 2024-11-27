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
            var query = (from cafe in _context.Cafes
                         join fileStore in _context.FileStores on cafe.LogoId equals fileStore.Id into fileStoreGroup
                         from fileStore in fileStoreGroup.DefaultIfEmpty()
                         join employee in _context.Employees on cafe.Id equals employee.CafeId into cafeEmployeesGroup
                         from employee in cafeEmployeesGroup.DefaultIfEmpty()
                         group employee by cafe into g
                         select new CafesByLocationResult
                         {
                             CafeId = g.Key.Id,
                             Description = g.Key.Description,
                             Name = g.Key.Name,
                             Location = g.Key.Location,
                             LogoPath = g.Key.Logo != null ? g.Key.Logo.FilePath : string.Empty,
                             EmployeeCount = g.Count(e => e != null)

                         }).OrderByDescending(o => o.EmployeeCount);

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}