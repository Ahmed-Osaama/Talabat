using Domain.Entities.OrderModule;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(o=>o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
