using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Cafes.Commands.Delete
{
    public class DeleteCafeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteCafeCommand>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();

                var cafe = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Cafe), request.Id);

                // modify cafe name
                cafe.Name = $"{cafe.Name}__deleted";

                _context.Cafes.Remove(cafe);
                await _context.SaveChangesAsync(cancellationToken);

                // modify email address
                List<Employee> employeesToDelete = await _context.Employees.Where(emp => emp.CafeId == request.Id).ToListAsync(cancellationToken: cancellationToken);
                employeesToDelete.ForEach(e =>
                {
                    e.EmailAddress = $"{e.EmailAddress}__deleted";
                });

                // raise domain event
                cafe.Raise(new CafeDeletedDomainEvent(cafe));

                _context.Employees.RemoveRange(employeesToDelete);
                await _context.SaveChangesAsync(cancellationToken);

                await _context.CommitTransactionAsync();
            }
            catch
            {
                await _context.RollbackTransactionAsync();
                throw;
            }
        }
    }
}