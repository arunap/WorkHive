using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Employees.Events
{
    public class EmployeeDeletedDomainEvent(Employee employee) : IDomainEvent
    {
        public Employee Employee { get; init; } = employee;
    }
}