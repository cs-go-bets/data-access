using System.Threading.Tasks;
using CSGOStats.Extensions.Validation;
using CSGOStats.Infrastructure.DataAccess.Contexts.EF;
using CSGOStats.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Infrastructure.DataAccess.Repositories.EF
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly BaseDataContext _context;

        public EfRepository(BaseDataContext context)
        {
            _context = context.NotNull(nameof(context));
        }

        public async Task<TEntity> FindAsync<TKey>(TKey id)
        {
            return await GetQueryable().FindAsync(id);
        }

        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            var result = await FindAsync(id);
            return result ?? throw EntityNotFound.For<TEntity>(id);
        }

        public async Task AddAsync<TKey>(TKey _, TEntity entity)
        {
            GetQueryable().Add(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync<TKey>(TKey _, TEntity entity)
        {
            GetQueryable().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync<TKey>(TKey _, TEntity entity)
        {
            GetQueryable().Remove(entity);
            await SaveChangesAsync();
        }

        private DbSet<TEntity> GetQueryable() => _context.Set<TEntity>();

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}