namespace WorkHive.Domain.Shared
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void RemoveDomainEvents() => _domainEvents.Clear();
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        // for concurrency check
        public byte[] RowVersion { get; set; } = [];
    }
}