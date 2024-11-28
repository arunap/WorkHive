namespace WorkHive.Application.Abstraction
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}