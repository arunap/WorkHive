using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkHive.Domain.Shared
{
    public class EntityBase<TKey>
    {
        public TKey Id { get; set; } = default!;

        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void RemoveDomainEvents() => _domainEvents.Clear();
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}