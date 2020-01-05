using CSGOStats.Infrastructure.DataAccess.Repositories;
using CSGOStats.Infrastructure.DataAccess.Repositories.EF;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Contexts;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Repositories
{
    public class TestRepository : EfRepository<TestEntity>
    {
        public TestRepository()
            : this(new TestEfContext())
        {
        }

        public TestRepository(TestEfContext context)
            : base(context)
        {
        }
    }
}