using MediatR;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Employees;

namespace WorkHive.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEmployeeIdProvider _employeeIdProvider;

        public CreateEmployeeCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider, IEmployeeIdProvider employeeIdProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
            _employeeIdProvider = employeeIdProvider;
        }

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
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }
    }
}