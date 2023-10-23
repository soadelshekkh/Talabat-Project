﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.services
{
    public interface Ipayment
    {
        Task<customerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> updatePaymentIntentSucceedOrFailed(string paymentIntentId, bool IsSuceeded);
    }
}
