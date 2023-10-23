using System.Collections.Generic;
using System;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public ICollection<OrdereItemDto> orderItem { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset orderDate { get; set; }
        public string OrderStatus { get; set; } 
        public Adrress ShippingAddress { get; set; }
        public string delivarymethod { get; set; }
        public decimal DeliverMethodCost { get; set; }
        public string PaymentIntedId { get; set; }
        public decimal Total { get; set; }

    }

}
