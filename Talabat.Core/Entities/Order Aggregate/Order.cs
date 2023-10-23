using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order :BaseEntity
    {
        public Order()
        {
                
        }

        public Order(string paymentIntendId, ICollection<OrderItem> orderItem, string buyerEmail, Adrress shippingAddress, Delivarymethod delivarymethod, decimal subTotalCost)
        {
            
            this.orderItem = orderItem;
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            this.delivarymethod = delivarymethod;
            SubTotalCost = subTotalCost;
            this.PaymentIntedId = paymentIntendId;
        }

        public ICollection< OrderItem> orderItem { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Adrress ShippingAddress { get; set; }
        public Delivarymethod delivarymethod { get; set; }
        public string PaymentIntedId { get; set; }
        public decimal SubTotalCost { get; set; }
        public decimal GetTotal()
        {
            return SubTotalCost + delivarymethod.Cost;
        }
    }
}
