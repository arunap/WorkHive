using MediatR;
using WorkHive.Application.Cafes.Queries.Dtos;

namespace WorkHive.Application.Cafes.Queries.GetById
{
    public class GetCafeByIdQuery : IRequest<CafeResult>
    {
        public Guid CafeId { get; set; }
    }
}