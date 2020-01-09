using System;

namespace CSGOStats.Infrastructure.DataAccess.Entities
{
    public interface IHaveIdEntity : IEntity
    {
        Guid Id { get; }
    }
}