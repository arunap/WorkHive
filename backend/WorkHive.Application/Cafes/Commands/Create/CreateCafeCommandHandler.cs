using MediatR;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;

namespace WorkHive.Application.Cafes.Commands.Create
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IImageUploader _imageUploader;

        public CreateCafeCommandHandler(IApplicationDbContext context, IImageUploader imageUploader)
        {
            _context = context;
            _imageUploader = imageUploader;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            Guid? imageId = null;
            if (request.Logo != null) imageId = await _imageUploader.UploadFileAsync(request.Logo);

            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                LogoId = imageId,
                Location = request.Location
            };

            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync(cancellationToken);

            return cafe.Id;
        }
    }
}