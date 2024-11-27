using WorkHive.Domain.Cafes;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.FileInfo
{
    public class FileStore : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }

        // Navigation properties
        public virtual ICollection<Cafe> Cafes { get; set; }
    }
}