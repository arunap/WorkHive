using MediatR;
using WorkHive.Application.Employees.Queries.Dtos;

namespace WorkHive.Application.Employees.Queries.GetById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeResult>
    {
        public string Id { get; set; } = null!;
    }
}