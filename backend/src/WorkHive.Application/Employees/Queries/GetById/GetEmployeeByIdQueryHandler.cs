using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Employees.Queries.Dtos;

namespace WorkHive.Application.Employees.Queries.GetById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResult>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeResult> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await (from employee in _context.Employees
                               join cafe in _context.Cafes on employee.CafeId equals cafe.Id into cafeGroup
                               from cafe in cafeGroup.DefaultIfEmpty()
                               where employee.Id == request.Id
                               select new EmployeeResult
                               {
                                   EmployeeId = employee.Id,
                                   Name = employee.Name,
                                   EmailAddress = employee.EmailAddress,
                                   Gender = employee.Gender,
                                   PhoneNumber = employee.PhoneNumber,
                                   CafeName = cafe.Name,
                                   StartedAt = employee.StartedAt,
                               })
                   .SingleOrDefaultAsync(cancellationToken);

            return query;

        }
    }
}