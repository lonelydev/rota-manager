using System;

namespace Domain
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTime Today { get; }
    }
}
