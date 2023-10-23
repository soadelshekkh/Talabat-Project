using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.services
{
    public interface IResponseCacheService
    {
        Task ResponseCacheAsync(string cacheKey, object Response, TimeSpan TimeToLive);
        Task<string> GetResponseCacheAsync(string cacheKey);
    }
}
