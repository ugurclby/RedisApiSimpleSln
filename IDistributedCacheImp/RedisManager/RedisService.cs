using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace IDistributedCacheImp.RedisManager
{
    public class RedisService : IRedisService 
    {
        private readonly IDistributedCache _distributedCache;
        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache; 
        } 
        public void Add(string key, object data,  DistributedCacheEntryOptions distributedCacheEntryOptions)
        {
              _distributedCache.SetString(key, data.ToJsonString(), distributedCacheEntryOptions);
        }
        public void Add(string key, byte[] data,  DistributedCacheEntryOptions distributedCacheEntryOptions)
        {
            _distributedCache.Set(key, data, distributedCacheEntryOptions);
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
    public static class Extension
    {
        public static string ToJsonString(this object data)
        {
            return  JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
