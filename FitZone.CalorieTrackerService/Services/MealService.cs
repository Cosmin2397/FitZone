using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FitZone.CalorieTrackerService.Services
{

    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly ICacheService _cacheService;

        public MealService(IMealRepository mealRepository, ICacheService cacheService)
        {
            _mealRepository = mealRepository;
            _cacheService = cacheService;
        }

        public async Task<DailyClientMeals> GetMealsAsync(Guid clientId, string date)
        {
            string cacheKey = $"meals_{clientId}_{date}";

            var cachedMeals = await _cacheService.GetCacheAsync<DailyClientMeals>(cacheKey);
            if (cachedMeals != null)
            {
                return cachedMeals;
            }

            var meals = await _mealRepository.GetMealsByClientAndDateAsync(clientId, date);
            if (meals != null)
            {
                await _cacheService.SetCacheAsync(cacheKey, meals, TimeSpan.FromMinutes(30));
            }

            return meals;
        }

        public async Task UpsertMealLogAsync(DailyClientMeals mealLog)
        {
            await _mealRepository.UpsertMealLogAsync(mealLog);

            // Șterge cache-ul pentru date actualizate
            string cacheKey = $"meals_{mealLog.ClientId}_{mealLog.Date}";
            await _cacheService.DeleteCacheAsync(cacheKey);
        }

        public async Task DeleteMealLogAsync(Guid clientId, string date)
        {
            await _mealRepository.DeleteMealLogAsync(clientId, date);

            // Șterge cache-ul pentru date șterse
            string cacheKey = $"meals_{clientId}_{date}";
            await _cacheService.DeleteCacheAsync(cacheKey);
        }
    }
}
