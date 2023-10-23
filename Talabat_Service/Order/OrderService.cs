using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Reposatiries;
using Talabat.Core.services;
using Talabat.Core.spcifications;

namespace Talabat_Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositary basketRepositary;
        private readonly IUnitOfWork unitOfWork;
        private readonly Ipayment payment;

        //private readonly IGenericRepositary<Product> productRepositary;
        //private readonly IGenericRepositary<Delivarymethod> deliverMethodRepositary;
        //private readonly IGenericRepositary<Talabat.Core.Entities.Order_Aggregate.Order> orderRepositary;
        //IGenericRepositary<Product> ProductRepositary,
        //IGenericRepositary<Delivarymethod> deliverMethodRepositary, IGenericRepositary<Talabat.Core.Entities.Order_Aggregate.Order> orderRepositary
        public OrderService(IBasketRepositary basketRepositary, IUnitOfWork unitOfWork, Ipayment payment)
        {
            this.basketRepositary = basketRepositary;
            this.unitOfWork = unitOfWork;
            this.payment = payment;
            //this.productRepositary = ProductRepositary;
            //this.deliverMethodRepositary = deliverMethodRepositary;
            //this.orderRepositary = orderRepositary;
        }
        public async Task<Talabat.Core.Entities.Order_Aggregate.Order> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethodId, Adrress shippingAddress)
        {
            //Get Basket
            var basket = await basketRepositary.GetCustomerBasket(basketId);
            // Get Products in Basket
            var OrderItems = new List<OrderItem>();
            foreach(var Item in basket.basketItems)
            {
                var product = await unitOfWork.Repositary<Product>().GetById(Item.Id);
                var orderItemsOrdered = new productItemOrdered(product.PictureUrl, product.Id, product.Name);
                var orderItem = new OrderItem(product.Price,Item.Quantity,orderItemsOrdered);
                OrderItems.Add(orderItem);
            }
            // subtotal
            var SubTotal = OrderItems.Sum(item => item.Quantity * item.Price);
            // deliveryMethod
            var DeliveryMethod = await unitOfWork.Repositary<Delivarymethod>().GetById(deliveryMethodId);

            PaymentIntendSpec spec = new PaymentIntendSpec(basket.PaymentIntent);
            var ExsitingOrder = await unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().GetByIdSpec(spec);
            if (ExsitingOrder != null)
            {
                unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().delete(ExsitingOrder);
                await payment.CreateOrUpdatePaymentIntent(basketId);
            }
            //Create order
            var Order = new Talabat.Core.Entities.Order_Aggregate.Order(basket.PaymentIntent ,OrderItems,BuyerEmail,shippingAddress,DeliveryMethod,SubTotal);
                await unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().Create(Order);
            var result = await unitOfWork.Complete();
            if (result < 1)
                return null;
            return Order;
        }

        public async Task<IReadOnlyList<Delivarymethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods =(IReadOnlyList<Delivarymethod>)await  unitOfWork.Repositary<Delivarymethod>().GetAll();
            return deliveryMethods;
        }

        public async Task<Talabat.Core.Entities.Order_Aggregate.Order> GetOrderByIdForUserAsync(string BuyerEmail, int orderId)
        {
            // where email and id 
            var spec = new GetOrdersWithDeliveryMethodAndItems(BuyerEmail, orderId);
            var order = await unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().GetByIdSpec(spec);
            return order;
        }

        public async Task<IReadOnlyList<Talabat.Core.Entities.Order_Aggregate.Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var spec = new GetOrdersWithDeliveryMethodAndItems(BuyerEmail);
            var orders =(IReadOnlyList<Talabat.Core.Entities.Order_Aggregate.Order>) await unitOfWork.Repositary<Talabat.Core.Entities.Order_Aggregate.Order>().GetAllSpec(spec);
            return orders;
        }
    }
}
