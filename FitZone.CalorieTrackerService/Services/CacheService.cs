using FitZone.CalorieTrackerService.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitZone.CalorieTrackerService.Services
{

    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
        {
            var jsonData = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            await _distributedCache.SetStringAsync(key, jsonData, options);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var data = await _distributedCache.GetStringAsync(key);
            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public async Task DeleteCacheAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }


}
