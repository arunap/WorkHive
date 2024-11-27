using MediatR;
using Microsoft.AspNetCore.Http;

namespace WorkHive.Application.Cafes.Commands.Create
{
    public class CreateCafeCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? Logo { get; set; } = null;
        public string Location { get; set; } = null!;
    }
}