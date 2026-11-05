using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;
using System.Collections.Concurrent;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext dbContext;
        private readonly ConcurrentDictionary<string, object> repositories = new();

        public UnitOfWork(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new();
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(dbContext)) as IGenericRepository<TEntity, TKey>;

        public async Task<int> SaveChangesAsync()
            =>await dbContext.SaveChangesAsync();

    }
}
