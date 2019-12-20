using System;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities
{
    public class TestEntity : IHaveIdEntity
    {
        public Guid Id { get; }

        public DateTime Date { get; private set; }

        public TestEntity(Guid id, DateTime date)
        {
            Id = id;
            Date = date;
        }

        public TestEntity Update()
        {
            Date = GetCurrentDate;
            return this;
        }

        public void Deconstruct(out Guid id, out DateTime date)
        {
            id = Id;
            date = Date;
        }

        public static TestEntity CreateEmpty() => new TestEntity(Guid.NewGuid(), GetEmptyDate);

        // TODO: move to shared 'date/time provider'
        private static DateTime GetCurrentDate => DateTime.UtcNow;

        // TODO: move to shared 'date/time provider'
        private static DateTime GetEmptyDate => DateTime.MinValue;
    }
}