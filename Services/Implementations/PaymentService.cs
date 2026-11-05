using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Specifications;
using Shared.DTOs.BasketModule;
using Stripe;
using Product = Domain.Entities.Product.Product;

namespace Services.Implementations
{
    public class PaymentService(IConfiguration _configuration,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork , IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            //0]Install Stripe.net package
            //1]Set Up Key [secret key] in appsettings.json
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["WebhookSecret"];
            //2]Get Basket [by BasketId]
            var Basket =await _basketRepository.GetBasketAsync(basketId) ??
                throw new BasketNotFoundException(basketId);
            //3]Validate Items Price ==> [Basket.Items.Price == Product.Price]
            foreach(var item in Basket.Items)
            {
                var product= await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            //4]Validate Shipping Price ==> Get DeliveryMethodId ==> shippingPrice = DeliveryMethod.Price
            if(!Basket.DeliveryMethodId.HasValue) throw new Exception("DeliveryMethodId is required");
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(Basket.DeliveryMethodId.Value) ??
                throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);
             Basket.ShippingPrice = DeliveryMethod.Price;
            //5] Calculate Total Price ==> (Long) shippping price + (items price * quantity) * 100
            var amount =(long) (Basket.Items.Sum(x=> x.Price * x.Quantity) + Basket.ShippingPrice) * 100;
            //6] Create or Update PaymentIntent
            var StripeService = new PaymentIntentService();
            if(string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var PaymentIntent = await StripeService.CreateAsync(new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                });
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await StripeService.UpdateAsync(Basket.PaymentIntentId, options);
            }
            //7] Save PaymentIntentId and ClientSecret in Basket
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            //8] Map to BasketDto and Return
            return _mapper.Map<BasketDto>(Basket);
        }

        public async Task UpdatePaymentStatusAsync(string json, string signatureHeader)
        {
                string endpointSecret = _configuration.GetSection("StripeSettings")["WebhookSecret"];

                var stripeEvent = EventUtility.ParseEvent(json,throwOnApiVersionMismatch:false);
            
                stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);
                var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                await UpdatePaymentStatusReceivedAsync(PaymentIntent.Id);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                await UpdatePaymentStatusFailedAsync(PaymentIntent.Id);
                }
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
           
        }

        private async Task UpdatePaymentStatusFailedAsync(string id)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(id));
            if(order != null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentFailed;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdatePaymentStatusReceivedAsync(string id)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(id));
            if (order != null)
            {
                order.PaymentStatus = OrderPaymentStatus.PaymentReceived;
                orderRepo.Update(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
