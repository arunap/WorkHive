using WorkHive.Domain.Employees;
using WorkHive.Domain.FileInfo;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Cafes
{
    public class Cafe : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        // Foreign key
        public Guid? LogoId { get; set; }

        // navigation properties
        public FileStore? Logo { get; set; }
        public ICollection<Employee> Employees { get; set; } = [];

    }
}