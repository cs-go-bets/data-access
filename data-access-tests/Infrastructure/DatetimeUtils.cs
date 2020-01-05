using System;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure
{
    // TODO: move to shared 'date/time provider'
    internal static class DatetimeUtils
    {
        internal static OffsetDateTime GetCurrentDate => OffsetDateTime.FromDateTimeOffset(DateTimeOffset.UtcNow);

        internal static OffsetDateTime GetEmptyDate => OffsetDateTime.FromDateTimeOffset(DateTimeOffset.MinValue);
    }
}