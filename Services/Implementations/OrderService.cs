using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderModule;
using Domain.Entities.Product;
using Domain.Exceptions;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DTOs.OrderModule;

namespace Services.Implementations
{
    public class OrderService(IMapper _mapper , IBasketRepository basketRepository , IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string Email)
        {
            //1] Map addressDto to address
            var address = _mapper.Map<Address>(orderRequest.ShipToAddress);
            //2] Get order items ==> BasketId ==> Basket ==> BasketItems [ID]
            var Items = new List<OrderItem>();
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                Items.Add(new OrderItem(new ProductInOrderItem(product.Id,product.Name, product.PictureUrl), product.Price, item.Quantity));
            }
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var OrderExist = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if (OrderExist != null)
            {
                orderRepo.Delete(OrderExist);
            }
            //3] Get delivery method ==> DeliveryMethodId ==> Db
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            //4] Calculate subtotal ==> OrderItems ==> Price * Quantity
            var subtotal = Items.Sum(i => i.Price * i.Quantity);
            //5] Create order ==> params, Add db , save changes
            var MainOrder = new Order(Email,address,Items,deliveryMethod,subtotal,basket.PaymentIntentId);
            await orderRepo.AddAsync(MainOrder);
            await unitOfWork.SaveChangesAsync();
            //6] map order to orderResult
            return _mapper.Map<OrderResult>(MainOrder);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodAsync()
        {
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(DeliveryMethod);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var Order =await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludeSpecifications(id) ?? 
                throw new OrderNotFoundException(id));
            return _mapper.Map<OrderResult>(Order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            var Orders =await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludeSpecifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(Orders);
        }
    }
}
