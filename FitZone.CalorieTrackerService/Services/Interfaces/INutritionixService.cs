using FitZone.CalorieTrackerService.Models;

namespace FitZone.CalorieTrackerService.Services.Interfaces
{
    public interface INutritionixService
    {
        Task<FoodItem> GetFoodByName(string food);


        FoodItem UpdateFoodSize(FoodItem item, double newPortionsSize);
    }
}
