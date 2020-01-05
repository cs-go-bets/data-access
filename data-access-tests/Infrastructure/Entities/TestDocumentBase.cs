using System;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Infrastructure.DataAccess.Tests.Infrastructure.Entities
{
    public abstract class TestDocumentBase : IHaveIdEntity
    {
        public Guid Id { get; private set; }

        protected TestDocumentBase(Guid id)
        {
            Id = id;
        }
    }
}