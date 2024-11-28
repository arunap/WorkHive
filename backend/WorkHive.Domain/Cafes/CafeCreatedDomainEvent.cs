using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Cafes
{
    public class CafeCreatedDomainEvent(Cafe cafe) : IDomainEvent
    {
        public Cafe Cafe { get; init; } = cafe;
    }
}