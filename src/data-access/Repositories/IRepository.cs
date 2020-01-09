using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess.Entities;

namespace CSGOStats.Infrastructure.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<TEntity> FindAsync<TKey>(TKey id);

        Task<TEntity> GetAsync<TKey>(TKey id);

        Task AddAsync<TKey>(TKey id, TEntity entity);

        Task UpdateAsync<TKey>(TKey id, TEntity entity);

        Task DeleteAsync<TKey>(TKey id, TEntity entity);
    }
}