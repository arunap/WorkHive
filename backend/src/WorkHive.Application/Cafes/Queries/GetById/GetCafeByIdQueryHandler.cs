using MediatR;
using WorkHive.Application.Cafes.Queries.Dtos;

namespace WorkHive.Application.Cafes.Queries.GetById
{
    public class GetCafeByIdQueryHandler : IRequestHandler<GetCafeByIdQuery, CafeResult>
    {
        public Task<CafeResult> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}