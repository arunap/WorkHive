using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkHive.Application.Abstraction;

namespace WorkHive.Infrastructure.Providers
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}