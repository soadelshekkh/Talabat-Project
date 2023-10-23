using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class productItemOrdered
    {
        public productItemOrdered(string pictureUrl, int productId, string productName)
        {
            PictureUrl = pictureUrl;
            ProductId = productId;
            ProductName = productName;
        }
        public productItemOrdered()
        {

        }
        public string PictureUrl { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
