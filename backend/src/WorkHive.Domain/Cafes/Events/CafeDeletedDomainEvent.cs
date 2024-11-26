using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkHive.Domain.Shared;

namespace WorkHive.Domain.Cafes.Events
{
    public class CafeDeletedDomainEvent(Cafe cafe) : IDomainEvent
    {
        public Cafe Cafe { get; init; } = cafe;
    }
}