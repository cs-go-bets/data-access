﻿using System.Threading.Tasks;
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

        public Task<TEntity> GetAsync<TKey>(TKey id) =>
            FindAsync(id).ContinueWith(x => 
                x.Result ?? 
                throw EntityNotFound.For<TEntity>(id));

        public async Task AddAsync<TKey>(TKey _, TEntity entity)
        {
            await GetQueryable().AddAsync(entity);
            await SaveChangesAsync();
        }

        public Task UpdateAsync<TKey>(TKey _, TEntity entity)
        {
            GetQueryable().Update(entity);
            return SaveChangesAsync();
        }

        public Task DeleteAsync<TKey>(TKey _, TEntity entity)
        {
            GetQueryable().Remove(entity);
            return SaveChangesAsync();
        }

        private DbSet<TEntity> GetQueryable() =>
            _context.Set<TEntity>();

        private Task SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}