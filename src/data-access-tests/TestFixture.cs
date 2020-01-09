using System;
using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.DataAccess.Repositories.Mongo;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Contexts;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Repositories;

namespace CSGOStats.Infrastructure.DataAccess.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly TestEfContext _efContext = new TestEfContext();
        private readonly TestMongoContext _mongoContext = new TestMongoContext();

        public IRepository<TestEntity> EntityRepository { get; }

        public IRepository<TestDocument> DocumentRepository { get; }

        public TestFixture()
        {
            _efContext.Database.EnsureCreated();

            EntityRepository = new TestRepository(_efContext);
            DocumentRepository = new GuidKeyMongoRepository<TestDocument>(_mongoContext);
        }

        public void Dispose()
        {
            _efContext.Database.EnsureDeleted();

            _efContext.Dispose();
            _mongoContext.Dispose();
        }
    }
}