namespace FitZone.CalorieTrackerService.Services.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object value, TimeSpan expiration);

        Task<T> GetCacheAsync<T>(string key);

        Task DeleteCacheAsync(string key);
    }
}
