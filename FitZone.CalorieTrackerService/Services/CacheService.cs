using FitZone.CalorieTrackerService.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitZone.CalorieTrackerService.Services
{


    public class CacheService: ICacheService
    {
        private readonly IDatabase _cacheDb;

        public CacheService(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect($"{config["RedisSettings:Host"]}:{config["RedisSettings:Port"]}");
            _cacheDb = redis.GetDatabase();
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _cacheDb.StringSetAsync(key, jsonData, expiration);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var data = await _cacheDb.StringGetAsync(key);
            return data.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(data);
        }

        public async Task DeleteCacheAsync(string key)
        {
            await _cacheDb.KeyDeleteAsync(key);
        }
    }

}
