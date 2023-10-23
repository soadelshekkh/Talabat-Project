using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Reposatiries
{
    public interface IBasketRepositary
    {
        Task<customerBasket> GetCustomerBasket(string basketId);
        Task<customerBasket> UpdateBasket(customerBasket Basket);
        Task<bool> DeleteBasket(string basketId); 
    }
}
