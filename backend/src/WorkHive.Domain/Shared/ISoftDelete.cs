namespace WorkHive.Domain.Shared
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}