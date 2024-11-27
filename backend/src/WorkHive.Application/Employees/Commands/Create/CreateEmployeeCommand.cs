using MediatR;
using WorkHive.Domain.Enums;

namespace WorkHive.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommand : IRequest<string>
    {
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
    }
}