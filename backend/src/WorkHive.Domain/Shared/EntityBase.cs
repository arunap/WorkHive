namespace WorkHive.Domain.Shared
{
    public interface IEntityBase;
    public class EntityBase : IEntityBase
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void RemoveDomainEvents() => _domainEvents.Clear();
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}