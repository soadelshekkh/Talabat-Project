using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.services;

namespace Talabat_Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase database;

        public ResponseCacheService(IConnectionMultiplexer Redis)
        {
            database = Redis.GetDatabase();
        }
        public async Task<string> GetResponseCacheAsync(string cacheKey)
        {
            var redisResponse = await database.StringGetAsync(cacheKey);
            if (redisResponse.IsNullOrEmpty) return null;
            return redisResponse;
        }

        public async Task ResponseCacheAsync(string cacheKey, object Response, TimeSpan TimeToLive)
        {
            if (Response == null) return;
            //var cacheKeyEncoded = Encoding.UTF8.GetBytes(cacheKey);
            var options = new JsonSerializerOptions() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var ResponseJson = JsonSerializer.Serialize(Response, options);
            await database.StringSetAsync(cacheKey, ResponseJson,TimeToLive);
        }
    }
}
