using WorkHive.Domain.Enums;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Employees
{
    public class EmployeeAudit : Employee, IAuditEntity
    {
        public RecordActionType ActionName { get; set; }
        public DateTime PerformedAt { get; set; }
        public static string PerformedBy => Environment.UserName;
    }
}