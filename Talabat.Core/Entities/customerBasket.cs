using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class customerBasket
    {
        public customerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<BasketItem> basketItems { get; set; } = new List<BasketItem>();
        public string PaymentIntent { get; set; }
        public string clientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
