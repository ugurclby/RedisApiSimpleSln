using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using IDistributedCacheImp.Helper; 

namespace IDistributedCacheImp.CacheManager
{
    public class RedisService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache; 
        } 
        public void Add(string key, object data, DateTimeOffset? absExpr, TimeSpan? sldExpr)
        {
              _distributedCache.SetString(key, data.ToJsonString(), new DistributedCacheEntryOptions() { AbsoluteExpiration = absExpr,SlidingExpiration=sldExpr });
        }
        public void Add(string key, byte[] data, DateTimeOffset? absExpr, TimeSpan? sldExpr)
        {
            _distributedCache.Set(key, data, new DistributedCacheEntryOptions() { AbsoluteExpiration = absExpr, SlidingExpiration = sldExpr });
        }
        public bool Any(string key)
        { 
             return String.IsNullOrEmpty(_distributedCache.GetString(key)) ? false:true;
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string data = _distributedCache.GetString(key);
                return JsonConvert.DeserializeObject<T>(data);
            }
            return default;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
    } 
}
