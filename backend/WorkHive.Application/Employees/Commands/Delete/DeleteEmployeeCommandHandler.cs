using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Employees.Commands.Delete
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Employee), request.Id);

            _context.Employees.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}