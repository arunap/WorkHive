using MediatR;
using WorkHive.Application.Abstraction.Context;

namespace WorkHive.Application.Cafes.Commands.Create
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCafeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}