using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Today => DateTime.Today;
    }
}
