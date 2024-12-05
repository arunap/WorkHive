using MediatR;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Employees;

namespace WorkHive.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider, IEmployeeIdProvider employeeIdProvider) : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
        private readonly IEmployeeIdProvider _employeeIdProvider = employeeIdProvider;

        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Id = await _employeeIdProvider.NextIdAsync(),
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Name = request.Name,
                CafeId = (request.CafeId.HasValue && request.CafeId != Guid.Empty) ? request.CafeId : null,
                StartedAt = request.CafeId.HasValue ? _dateTimeProvider.UtcNow : null
            };

            employee.Raise(new EmployeeCreatedDomainEvent(employee));
            
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}