using FitZone.CalorieTrackerService.Models;

namespace FitZone.CalorieTrackerService.Services.Interfaces
{
    public interface IMealService
    {
        Task<DailyClientMeals> GetMealsAsync(Guid clientId, DateTime date);

        Task UpsertMealLogAsync(DailyClientMeals mealLog);

        Task DeleteMealLogAsync(Guid clientId, DateTime date);
    }
}
