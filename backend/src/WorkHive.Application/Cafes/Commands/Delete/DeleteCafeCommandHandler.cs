using MediatR;

namespace WorkHive.Application.Cafes.Commands.Delete
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        public Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}