﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class OrderItem :BaseEntity
    {
        public OrderItem(decimal price, int quantity, productItemOrdered productItemOrdered)
        {
            Price = price;
            Quantity = quantity;
            ProductItemOrdered = productItemOrdered;
        }
        public OrderItem()
        {

        }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public productItemOrdered ProductItemOrdered { get; set; }
    }
}
