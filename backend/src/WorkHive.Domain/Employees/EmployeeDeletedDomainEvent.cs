using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Employees
{
    public class EmployeeDeletedDomainEvent(Employee employee) : IDomainEvent
    {
        public Employee Employee { get; init; } = employee;
    }
}