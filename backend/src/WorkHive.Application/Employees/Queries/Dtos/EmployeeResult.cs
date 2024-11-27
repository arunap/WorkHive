using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkHive.Domain.Enums;

namespace WorkHive.Application.Employees.Queries.Dtos
{
    public class EmployeeResult
    {
        public string EmployeeId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public Guid? CafeId { get; set; }
        public string? CafeName { get; internal set; }
        public DateTime? StartedAt { get; set; }
    }
}