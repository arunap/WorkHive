using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Cafes.Commands.Update
{
    public class UpdateCafeCommandHandler(IApplicationDbContext context, IImageUploader imageUploader) : IRequestHandler<UpdateCafeCommand>
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IImageUploader _imageUploader = imageUploader;

        public async Task Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Cafes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Cafe), request.Id); ;
            var img = item.LogoId.HasValue ? await _context.FileStores.FirstOrDefaultAsync(i => i.Id == item.LogoId.Value, cancellationToken: cancellationToken) : null;

            Guid? imageId = null;
            if (request.Logo != null && request.Logo.FileName != img?.FileName) // upload if the image is difference (TODO: checksum)
                imageId = await _imageUploader.UploadFileAsync(request.Logo);

            item.LogoId = imageId ?? item.LogoId;
            item.Name = request.Name;
            item.Location = request.Location;
            item.Description = request.Description;

            // raise domain event
            item.Raise(new CafeCreatedDomainEvent(item));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}