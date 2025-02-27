using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services.Interfaces;
using System;
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

        public async Task<DailyClientMeals> GetMealsAsync(Guid clientId, DateTime date)
        {
            string cacheKey = $"meals_{clientId}_{date:yyyy-MM-dd}";

            //Verifică cache-ul Redis înainte de a accesa MongoDB
            var cachedMeals = await _cacheService.GetCacheAsync<DailyClientMeals>(cacheKey);
            if (cachedMeals != null)
            {
                return cachedMeals;
            }

            // Caută în MongoDB dacă nu este în cache
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

            // Șterge cache-ul pentru a ne asigura că frontend-ul primește date actualizate
            string cacheKey = $"meals_{mealLog.ClientId}_{mealLog.Date:yyyy-MM-dd}";
            await _cacheService.DeleteCacheAsync(cacheKey);
        }

        public async Task DeleteMealLogAsync(Guid clientId, DateTime date)
        {
            await _mealRepository.DeleteMealLogAsync(clientId, date);

            // Ștergem cache-ul
            string cacheKey = $"meals_{clientId}_{date:yyyy-MM-dd}";
            await _cacheService.DeleteCacheAsync(cacheKey);
        }
    }

}
