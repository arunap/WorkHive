using WorkHive.Domain.Enums;

namespace WorkHive.Domain.Shared
{
    public interface IAuditEntity
    {
        RecordActionType ActionName { get; set; }
        DateTime PerformedAt { get; set; }
        static string PerformedBy => Environment.UserName;
    }
}