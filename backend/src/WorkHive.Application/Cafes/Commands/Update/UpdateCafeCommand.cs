using MediatR;
using Microsoft.AspNetCore.Http;

namespace WorkHive.Application.Cafes.Commands.Update
{
    public class UpdateCafeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Logo { get; set; } = null;
        public string Location { get; set; } = string.Empty;
    }
}