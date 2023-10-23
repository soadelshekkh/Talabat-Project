using System.Collections.Generic;
using Talabat.Core.Entities;

namespace Talabat.Api.DTOS
{
    public class CustomerBasketDTO
    {
        public CustomerBasketDTO(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<BasketItemDTO> basketItems { get; set; } = new List<BasketItemDTO>();
        public string PaymentIntent { get; set; }
        public string clientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
