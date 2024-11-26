using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace WorkHive.Domain.Shared
{
    public interface IDomainEvent : INotification;
}