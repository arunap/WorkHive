using WorkHive.Domain.Cafes;
using WorkHive.Domain.Enums;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Employees
{
    public class Employee : EntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; } = GenderType.Male;
        public DateTime? StartedAt { get; set; }


        // navigation properties
        public Guid? CafeId { get; set; }
        public Cafe? Cafe { get; set; }
    }
}