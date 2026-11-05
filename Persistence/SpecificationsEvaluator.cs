using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery 
            , ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if(specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            if (specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0)
                query = specifications.IncludeExpression.Aggregate(query, (current, expression) => current.Include(expression));
            if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            if(specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);
            if(specifications.IsPaginated)
                query = query.Skip(specifications.Skip).Take(specifications.Take);


            return query;
        }
    }
}
