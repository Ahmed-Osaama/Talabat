using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; }
        public List<Expression<Func<TEntity,object>>> IncludeExpression { get; }
        public Expression<Func<TEntity,object>> OrderBy { get; }
        public Expression<Func<TEntity,object>> OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }
    }
}
