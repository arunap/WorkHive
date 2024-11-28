using MediatR;

namespace WorkHive.Application.Employees.Commands.Delete
{
    public class DeleteEmployeeCommand: IRequest
    {
          public string Id { get; set; }
    }
}