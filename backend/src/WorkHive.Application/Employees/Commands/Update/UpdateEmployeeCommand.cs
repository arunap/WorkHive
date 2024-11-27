using MediatR;
using WorkHive.Domain.Enums;

namespace WorkHive.Application.Employees.Commands.Update
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string EmployeeId { get; set; }
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
    }
}