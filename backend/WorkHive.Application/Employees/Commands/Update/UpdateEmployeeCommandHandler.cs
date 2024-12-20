using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Employees.Commands.Update
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Employee), request.EmployeeId);

            item.Name = request.Name;
            item.EmailAddress = request.EmailAddress;
            item.PhoneNumber = request.PhoneNumber;
            item.Gender = request.Gender;
            item.CafeId = (request.CafeId.HasValue && request.CafeId != Guid.Empty) ? request.CafeId : null;
            item.StartedAt = (request.CafeId.HasValue && item.CafeId != request.CafeId.Value) ? _dateTimeProvider.UtcNow : item.StartedAt;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}