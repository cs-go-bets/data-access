using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.DataAccess.Contexts;
using CSGOStats.Infrastructure.DataAccess.Entities;
using CSGOStats.Infrastructure.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CSGOStats.Infrastructure.DataAccess.Repositories
{
    public abstract class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly BaseDataContext _context;

        protected EfRepository(BaseDataContext context)
        {
            _context = context.NotNull(nameof(context));
        }

        public Task<IReadOnlyList<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> query) => throw new NotSupportedException();

        public Task<TIdEntity> FindAsync<TIdEntity>(Guid id) 
            where TIdEntity : class, IHaveIdEntity, TEntity =>
                GetSetQueryable<TIdEntity>().FindAsync(id);

        public async Task<TIdEntity> GetAsync<TIdEntity>(Guid id) 
            where TIdEntity : class, IHaveIdEntity, TEntity
        {
            var entity = await FindAsync<TIdEntity>(id);
            return entity ?? throw new EntityNotFound(typeof(TIdEntity).FullName, id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await GetQueryable().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            GetQueryable().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            GetQueryable().Remove(entity);
            await SaveChangesAsync();
        }

        private DbSet<TEntity> GetQueryable() => GetSetQueryable<TEntity>();

        private DbSet<TSet> GetSetQueryable<TSet>()
            where TSet : class
                => _context.Set<TSet>();

        private Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}