using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Application.Cafes.Queries.Dtos;
using WorkHive.Domain.Cafes;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Application.Cafes.Queries.GetById
{
    public class GetCafeByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetCafeByIdQuery, CafeResult>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<CafeResult> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _context.Cafes.Include(s => s.Logo).FirstOrDefaultAsync(c => c.Id == request.CafeId, cancellationToken: cancellationToken) ?? throw new ItemNotFoundException(nameof(Cafe), request.CafeId);

            return new CafeResult
            {
                CafeId = item.Id,
                Name = item.Name,
                Description = item.Description,
                Location = item.Location,
                LogoPath = item.Logo?.FilePath ?? string.Empty,
            };
        }
    }
}