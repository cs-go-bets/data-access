using System;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess.Entities;
using CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities;
using FluentAssertions;
using Xunit;

namespace CSGOStats.Infrastructure.DataAccess.Tests
{
    public class DatabaseCommunicationTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public DatabaseCommunicationTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AtomicCreateReadUpdateDeleteTest()
        {
            var entity = CreateTestEntity();
            var (entityId, entityDate) = entity;

            await _fixture.Repository.AddAsync(entity);

            var storedEntity = await _fixture.Repository.GetAsync<TestEntity>(entityId);
            storedEntity.Date.Should().Be(entityDate);

            storedEntity.Update();
            var updatedDate = storedEntity.Date;
            await _fixture.Repository.UpdateAsync(storedEntity);

            var updatedEntity = await _fixture.Repository.GetAsync<TestEntity>(entityId);
            updatedEntity.Date.Should().Be(updatedDate).And.NotBe(entityDate);

            await _fixture.Repository.DeleteAsync(updatedEntity);
            await _fixture.Repository.FindAsync<TestEntity>(entityId).ContinueWith(x => x.Result.Should().BeNull());

            var exception = await Record.ExceptionAsync(() => _fixture.Repository.GetAsync<TestEntity>(entityId))
                .ContinueWith(x => x.Result.Should().BeOfType<EntityNotFound>().Subject);
            exception.Type.Should().Be(typeof(TestEntity).FullName);
            exception.Id.Should().BeOfType<Guid>().And.Be(entityId);
        }

        private static TestEntity CreateTestEntity() => TestEntity.CreateEmpty();
    }
}