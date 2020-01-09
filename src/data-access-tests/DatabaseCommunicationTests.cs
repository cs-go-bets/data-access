using System;
using System.Threading.Tasks;
using CSGOStats.Extensions.Extensions;
using CSGOStats.Infrastructure.DataAccess.Entities;
using CSGOStats.Infrastructure.DataAccess.Repositories;
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
        public Task RelationalAtomicCrudTests()
        {
            var entity = CreateTestEntity();
            var (entityId, entityDate) = (entity.Id, entity.Date);

            return AtomicCrudTestsTemplate(
                entityId,
                entity,
                _fixture.EntityRepository,
                x => x.Update(),
                x => x.Date.Should().Be(entityDate),
                (x, y) => x.Date.Should().Be(y.Date).And.NotBe(entityDate));
        }

        [Fact]
        public Task DocumentOrientedAtomicCrudTests()
        {
            var document = CreateTestDocument();
            var clone = document.Clone().OfType<TestDocument>();

            return AtomicCrudTestsTemplate(
                document.Id,
                document,
                _fixture.DocumentRepository,
                x => x.Update(),
                x =>
                {
                    x.Inner.Data.Should().Be(clone.Inner.Data);
                    x.Inner.Count.Should().Be(clone.Inner.Count);
                    x.Version.Should().Be(clone.Version);
                    x.UpdatedOn.Should().Be(clone.UpdatedOn);
                },
                (x, y) =>
                {
                    x.Inner.Data.Should().Be(x.Inner.Data).And.NotBe(clone.Inner.Data);
                    x.Inner.Count.Should().Be(x.Inner.Count).And.NotBe(clone.Inner.Count);
                    x.Version.Should().Be(x.Version).And.NotBe(clone.Version);
                    x.UpdatedOn.Should().Be(x.UpdatedOn).And.NotBe(clone.UpdatedOn);
                });
        }

        private static async Task AtomicCrudTestsTemplate<TKey, TState>(
            TKey key,
            TState initialState,
            IRepository<TState> repository,
            Action<TState> updateEntityFunctor,
            Action<TState> storeVerificationFunctor,
            Action<TState, TState> updateVerificationFunctor)
                where TState : class, IEntity
        {
            await repository.AddAsync(key, initialState);

            var storedState = await repository.GetAsync(key);
            storeVerificationFunctor(storedState);

            updateEntityFunctor(storedState);
            await repository.UpdateAsync(key, storedState);

            var updatedState = await repository.GetAsync(key);
            updateVerificationFunctor(updatedState, storedState);

            await repository.DeleteAsync(key, updatedState);
            await repository.FindAsync(key).ContinueWith(x => x.Result.Should().BeNull());

            var exception = await Record.ExceptionAsync(() => repository.GetAsync(key))
                .ContinueWith(x => x.Result.Should().BeOfType<EntityNotFound>().Subject);
            exception.Type.Should().Be(typeof(TState).FullName);
            exception.Id.Should().BeOfType<Guid>().And.Be(key);
        }

        private static TestEntity CreateTestEntity() => TestEntity.CreateEmpty();

        private static TestDocument CreateTestDocument() => TestDocument.CreateRandomDocument();
    }
}