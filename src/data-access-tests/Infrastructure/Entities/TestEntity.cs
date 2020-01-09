using System;
using CSGOStats.Infrastructure.DataAccess.Entities;
using NodaTime;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities
{
    public class TestEntity : IHaveIdEntity
    {
        public Guid Id { get; }

        public OffsetDateTime Date { get; private set; }

        public TestEntity(Guid id, OffsetDateTime date)
        {
            Id = id;
            Date = date;
        }

        public void Update()
        {
            Date = DatetimeUtils.GetCurrentDate;
        }

        public static TestEntity CreateEmpty() => new TestEntity(Guid.NewGuid(), DatetimeUtils.GetEmptyDate);
    }
}