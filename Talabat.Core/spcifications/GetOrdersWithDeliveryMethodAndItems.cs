using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.spcifications
{
    public class GetOrdersWithDeliveryMethodAndItems : BaseSpecificaton<Order>
    {
        public GetOrdersWithDeliveryMethodAndItems(string BuyerMail):base(o => o.BuyerEmail == BuyerMail)
        {
            Includes.Add(o => o.delivarymethod);
            Includes.Add(o => o.orderItem);
           
        }
        public GetOrdersWithDeliveryMethodAndItems(string BuyerMail, int id) : base(
            o => o.BuyerEmail == BuyerMail && o.Id == id)
        {
            Includes.Add(o => o.delivarymethod);
            Includes.Add(o => o.orderItem);

        }
    }
}
