using WorkHive.Domain.Cafes;
using WorkHive.Domain.Shared;
using WorkHive.Domain.Shared.Enums;

namespace WorkHive.Domain.Employees
{
    public class Employee : BaseAuditableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? StartedAt { get; set; }

        // foreign key
        public Guid? CafeId { get; set; }

        // navigation properties
        public Cafe Cafe { get; set; }
    }
}