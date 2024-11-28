namespace WorkHive.Domain.Shared
{
    public class BaseAuditableEntity: BaseEntity
    {
        // audit properties
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}