using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkHive.Application.Abstraction
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}