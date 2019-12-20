using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Infrastructure.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        [Obsolete("So far there is no sense in having this method")]
        Task<IReadOnlyList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> query);

        Task<TIdEntity> FindAsync<TIdEntity>(Guid id) where TIdEntity : class, IHaveIdEntity, TEntity;

        Task<TIdEntity> GetAsync<TIdEntity>(Guid id) where TIdEntity : class, IHaveIdEntity, TEntity;

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}