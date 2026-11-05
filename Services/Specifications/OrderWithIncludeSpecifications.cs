using Domain.Entities.OrderModule;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithIncludeSpecifications(Guid id) : base(o=>o.Id == id)
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
        }
        public OrderWithIncludeSpecifications(string Email) : base(o => o.UserEmail == Email)
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
