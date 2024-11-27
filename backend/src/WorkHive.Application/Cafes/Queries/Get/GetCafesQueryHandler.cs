using MediatR;
using WorkHive.Application.Cafes.Queries.Dtos;

namespace WorkHive.Application.Cafes.Queries.Get
{
    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, CafesByLocationResult>
    {
        public Task<CafesByLocationResult> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}