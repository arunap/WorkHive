using MediatR;

namespace WorkHive.Application.Cafes.Commands.Update
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand>
    {
        public Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}