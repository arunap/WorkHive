using WorkHive.Application.Abstraction;

namespace WorkHive.Infrastructure.Providers
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}