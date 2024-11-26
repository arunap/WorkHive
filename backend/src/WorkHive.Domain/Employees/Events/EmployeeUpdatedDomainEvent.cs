using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkHive.Domain.Employees;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Employees.Events
{
    public class EmployeeUpdatedDomainEvent(Employee employee) : IDomainEvent
    {
        public Employee Employee { get; init; } = employee;
    }
}