using System;
using CSGOStats.Infrastructure.DataAccess.Contexts;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Contexts
{
    public class TestContext : BaseDataContext
    {
        public TestContext()
            : base(new PostgreConnectionSettings(
                host: "127.0.0.1",
                database: $"Test-{DateTime.UtcNow.Ticks}",
                username: "postgres",
                password: "dotFive1"))
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
        }
    }
}