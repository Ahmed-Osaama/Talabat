using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        => await _dbContext.Set<TEntity>().AddAsync(entity);

        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
            => SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();


        public void Delete(TEntity entity)
        => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        => asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() : await _dbContext.Set<TEntity>().ToListAsync();

        public Task<TEntity?> GetByIdAsync(TKey id)
        => _dbContext.Set<TEntity>().FindAsync(id).AsTask();

        public void Update(TEntity entity)
        => _dbContext.Set<TEntity>().Update(entity);

        async Task<IEnumerable<TEntity>> IGenericRepository<TEntity, TKey>.GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specifications).ToListAsync();

        async Task<TEntity?> IGenericRepository<TEntity, TKey>.GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
    }
}
