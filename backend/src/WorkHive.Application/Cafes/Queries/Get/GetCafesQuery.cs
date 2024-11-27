using MediatR;
using WorkHive.Application.Cafes.Queries.Dtos;

namespace WorkHive.Application.Cafes.Queries.Get
{
    public class GetCafesQuery : IRequest<List<CafesByLocationResult>>
    {
        public string? Location { get; set; }
    }
}