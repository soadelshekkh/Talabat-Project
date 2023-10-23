using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.spcifications
{
    public class PaymentIntendSpec : BaseSpecificaton<Order>
    {
        public PaymentIntendSpec(string PaymentIntendId) : base ( o => o.PaymentIntedId == PaymentIntendId)
        {

        }
    }
}
