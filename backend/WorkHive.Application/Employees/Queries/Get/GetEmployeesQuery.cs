using MediatR;
using WorkHive.Application.Employees.Queries.Dtos;

namespace WorkHive.Application.Employees.Queries.Get
{
    public class GetEmployeesQuery : IRequest<List<EmployeesByCafeNameResult>>
    {
        public string? CafeName { get; set; }
    }
}