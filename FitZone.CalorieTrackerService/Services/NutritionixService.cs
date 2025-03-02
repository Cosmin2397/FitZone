using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitZone.CalorieTrackerService.Services
{
    public class NutritionixService: INutritionixService
    {
        private readonly INutritionixRepository _nutritionixRepository;
        private readonly ICacheService _cacheService;

        public NutritionixService(INutritionixRepository nutritionixRepository, ICacheService cacheService)
        {
            _nutritionixRepository = nutritionixRepository;
            _cacheService = cacheService;
        }

        public async Task<FoodItem> GetFoodByName(string food)
        {
            string cacheKey = $"foodByName_{food}";

            //Verifică cache-ul Redis înainte de a accesa MongoDB
            var cachedMeal = await _cacheService.GetCacheAsync<FoodItem>(cacheKey);

            if (cachedMeal != null)
            {
                return cachedMeal;
            }
            else
            {
                var apiMeal = await _nutritionixRepository.GetFoodByName(food);
                if (apiMeal != null)
                {
                    await _cacheService.SetCacheAsync(cacheKey, apiMeal, TimeSpan.FromMinutes(30));
                    return apiMeal;
                }
                return null;
            }
        }

        public FoodItem UpdateFoodSize(FoodItem item, double newPortionsSize)
        {
            return _nutritionixRepository.UpdateFoodSize(item, newPortionsSize);
        }
    }
}
