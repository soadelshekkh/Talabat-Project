using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, string basketId, int deliveryMethodId, Adrress shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail);
        Task<Order> GetOrderByIdForUserAsync(string BuyerEmail, int orderId);
        Task<IReadOnlyList<Delivarymethod>> GetDeliveryMethodsAsync();
    }
}
