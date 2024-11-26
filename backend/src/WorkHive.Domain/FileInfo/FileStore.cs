using WorkHive.Domain.Shared;

namespace WorkHive.Domain.FileInfo
{
    public class FileStore : EntityBase
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}