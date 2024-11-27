using MediatR;

namespace WorkHive.Application.Cafes.Commands.Delete
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}