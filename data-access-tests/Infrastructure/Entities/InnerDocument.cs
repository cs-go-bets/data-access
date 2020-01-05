using System;
using CSGOStats.Extensions.Validation;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities
{
    public class InnerDocument : ICloneable
    {
        public string Data { get; private set; }

        public int Count { get; private set; }

        public OffsetDateTime? LockDate { get; private set; }

        public InnerDocument(string data, int count, OffsetDateTime? lockDate)
        {
            Data = data.NotNull(nameof(data));
            Count = count;
            LockDate = lockDate;
        }

        public void Update()
        {
            Data = Guid.NewGuid().ToString("N");
            Count = Guid.NewGuid().GetHashCode();
            LockDate = DatetimeUtils.GetCurrentDate;
        }

        public object Clone() => new InnerDocument(
            data: Data,
            count: Count,
            lockDate: null);
    }
}