using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheImp.CacheManager
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Add(string key, object data, DateTimeOffset? absExpr, TimeSpan? sldExpr); 
        bool Any(string key);
        void Remove(string key);
    }
}
