using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace StackExchangeImp.RedisManager
{
    public class RedisService : ICacheService
    {
        private readonly RedisConnDb _redisConnDb;
        private readonly IDatabase _db;
        public RedisService(RedisConnDb redisConnDb)
        {
            _redisConnDb = redisConnDb;
            _redisConnDb.Connect();
            _db = _redisConnDb.GetDb(0);
        }

        public Task AddAsyncString<T>(string key, T value, TimeSpan? sldExp)
        {
            throw new NotImplementedException();
        }

        public void AddString<T>(string key, T value, TimeSpan? sldExp)
        {
            _db.StringSet(key, JsonConvert.SerializeObject(value), sldExp);
        }

        public Task DeleteAsyncString(string key)
        {
            throw new NotImplementedException();
        }

        public void DeleteString(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsyncString<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetString<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(string key)
        {
            throw new NotImplementedException();
        }
    }
}
