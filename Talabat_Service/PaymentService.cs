using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Reposatiries;
using Talabat.Core.services;
using Stripe;
using Product = Talabat.Core.Entities.Product;
using Talabat.Core.spcifications;

namespace Talabat_Service
{
    public class PaymentService : Ipayment
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepositary basketRepositary;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepositary basketRepositary, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepositary = basketRepositary;
            this.unitOfWork = unitOfWork;
        }
        public async Task<customerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            
            var Basket = await basketRepositary.GetCustomerBasket(basketId);
            if(Basket == null) return null;
            var DeliveryMethodCost = 0m;
            if (Basket.DeliveryMethodId.HasValue)
            { 
                var deliveryMethod = await unitOfWork.Repositary<Delivarymethod>().GetById(Basket.DeliveryMethodId.Value);
                Basket.ShippingPrice = deliveryMethod.Cost;
                DeliveryMethodCost = deliveryMethod.Cost;
            }
            foreach(var Item in Basket.basketItems)
            {
                var product = await unitOfWork.Repositary<Product>().GetById(Item.Id);
                Item.price = product.Price;
            }
            // create payment intent
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if(string.IsNullOrEmpty(Basket.PaymentIntent))
            {
                var options = new PaymentIntentCreateOptions() 
                {
                    Amount =(long) Basket.basketItems.Sum(Item => Item.Quantity * Item.price * 100) +(long) DeliveryMethodCost,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                intent = await service.CreateAsync(options);
                Basket.PaymentIntent = intent.Id;
                Basket.clientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Basket.basketItems.Sum(Item => Item.Quantity * Item.price * 100) + (long)DeliveryMethodCost
                };
                intent = await service.UpdateAsync(Basket.PaymentIntent, options);
            }
            await basketRepositary.UpdateBasket(Basket);
            return Basket;
        }

        public async Task<Talabat.Core.Entities.Order_Aggregate.Order> updatePaymentIntentSucceedOrFailed(string paymentIntentId, bool IsSuceeded)
        {
            var spec = new PaymentIntendSpec(paymentIntentId);
            var order = await unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().GetByIdSpec(spec);
            if(IsSuceeded)
            {
                order.OrderStatus = OrderStatus.Recieved;
            }
            else
            {
                order.OrderStatus = OrderStatus.PaymentFailed;
            }
            unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().update(order);
            await unitOfWork.Complete();
            return order;
        }

    }
}
