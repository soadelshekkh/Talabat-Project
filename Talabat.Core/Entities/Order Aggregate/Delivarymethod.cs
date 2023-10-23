using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Delivarymethod : BaseEntity
    {
       
        public Delivarymethod()
        {

        }

        public Delivarymethod(decimal cost, string shortName, string deliveryTime, string description)
        {
            Cost = cost;
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Description = description;
        }

        public decimal Cost { get; set; }
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
    }
}
