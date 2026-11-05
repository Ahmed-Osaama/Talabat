using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        #region Criteria
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region Includes
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = new();


        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
            => IncludeExpression.Add(includeExpression);
        #endregion

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderBy = orderByExpression;

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
            => OrderByDescending = orderByDescendingExpression;
        #endregion

        #region Pagination
        public int Take { get; private set; }
        protected void ApplyPagination(int Pagesize, int PageIndex)
        {
            IsPaginated = true;
            Take = Pagesize;
            Skip = Pagesize * (PageIndex - 1);
        }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }
        #endregion
    }
}
