using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOS
{
    public class OrderDTO
    {
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public Adrress ShippingAddress { get; set; }
    }
}
