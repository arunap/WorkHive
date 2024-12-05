using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Employees.Queries.Dtos;

namespace WorkHive.Application.Employees.Queries.Get
{
    public class GetEmployeesQueryHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider) : IRequestHandler<GetEmployeesQuery, List<EmployeesByCafeNameResult>>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        public async Task<List<EmployeesByCafeNameResult>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            DateTime today = _dateTimeProvider.UtcNow;
            var query = await (from employee in _context.Employees
                               join cafe in _context.Cafes on employee.CafeId equals cafe.Id into cafeGroup
                               from cafe in cafeGroup.DefaultIfEmpty()
                               where string.IsNullOrEmpty(request.CafeName) || cafe.Name == request.CafeName
                               select new EmployeesByCafeNameResult
                               {
                                   EmployeeId = employee.Id,
                                   Name = employee.Name,
                                   EmailAddress = employee.EmailAddress,
                                   Gender = employee.Gender,
                                   PhoneNumber = employee.PhoneNumber,
                                   DaysWorked = employee.StartedAt.HasValue ? (today - employee.StartedAt.Value).Days : 0,
                                   CafeName = cafe.Name,
                                   StartedAt = employee.StartedAt,
                               })
                        .ToListAsync(cancellationToken);

            return [.. query.OrderByDescending(e => e.DaysWorked)];
        }
    }
}