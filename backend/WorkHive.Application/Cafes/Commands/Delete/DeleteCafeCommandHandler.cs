using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Cafes.Commands.Delete
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCafeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();

                var cafe = await _context.Cafes
                .Include(cafes => cafes.Employees)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Cafe), request.Id);

                // modify cafe name
                cafe.Name = $"{cafe.Name}__deleted";
                
                // modify email address
                cafe.Employees.ToList().ForEach(e =>
                {
                    e.EmailAddress = $"{e.EmailAddress}__deleted";
                });

                _context.Employees.RemoveRange(cafe.Employees);
                await _context.SaveChangesAsync(cancellationToken);

                _context.Cafes.Remove(cafe);
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