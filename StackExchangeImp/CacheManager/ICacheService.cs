using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace StackExchangeImp.RedisManager
{
    public interface ICacheService
    {
        void AddString<T>(string key, T value, TimeSpan? sldExp);
        Task AddAsyncString<T>(string key, T value, TimeSpan? sldExp); 
        T GetString<T>(string key);
        Task<T> GetAsyncString<T>(string key); 
        void DeleteString(string key);
        Task DeleteAsyncString(string key); 
        bool IsExists(string key); 
    }
}
