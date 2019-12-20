using System;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Contexts;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Repositories;

namespace CSGOStats.Infrastructure.DataAccess.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly TestContext _context = new TestContext();

        public IRepository<TestEntity> Repository { get; }

        public TestFixture()
        {
            _context.Database.EnsureCreated();

            Repository = new TestRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }
    }
}