using WorkHive.Domain.Employees;
using WorkHive.Domain.FileInfo;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Cafes
{
    public class Cafe : EntityBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? LogoId { get; set; }
        public FileStore LogoInfo { get; set; }
        public string Location { get; set; }

        // navigation properties
        public ICollection<Employee> Employees { get; set; } = [];

    }
}