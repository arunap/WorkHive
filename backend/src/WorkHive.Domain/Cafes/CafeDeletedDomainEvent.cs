using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Cafes
{
    public class CafeDeletedDomainEvent(Cafe cafe) : IDomainEvent
    {
        public Cafe Cafe { get; init; } = cafe;
    }
}