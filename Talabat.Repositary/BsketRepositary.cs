using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Reposatiries;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Talabat.Repositary
{
    public class BsketRepositary : IBasketRepositary
    {
        private readonly IDatabase database;
        public BsketRepositary(IConnectionMultiplexer Redis)
        {
            database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string basketId)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<customerBasket> GetCustomerBasket(string basketId)
        {
            var basket = await database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<customerBasket>(basket);
        }

        public async Task<customerBasket> UpdateBasket(customerBasket Basket)
        {
            var createOrUpdate = await database.StringSetAsync(Basket.Id, JsonSerializer.Serialize(Basket),TimeSpan.FromDays(10));
            if(createOrUpdate)
                return await GetCustomerBasket(Basket.Id);
            else
                return null;
        }
    }
}
