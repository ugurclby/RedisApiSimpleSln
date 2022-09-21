using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.RedisManager
{
    public interface IRedisService
    {
        T Get<T>(string key);
        void Add(string key, object data, [FromServices] DistributedCacheEntryOptions distributedCacheEntryOptions); 
        bool Any(string key);
        void Remove(string key);
    }
}
