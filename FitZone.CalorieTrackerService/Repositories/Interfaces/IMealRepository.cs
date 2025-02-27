using FitZone.CalorieTrackerService.Models;

namespace FitZone.CalorieTrackerService.Repositories.Interfaces
{
    public interface IMealRepository
    {
        Task<DailyClientMeals> GetMealsByClientAndDateAsync(Guid clientId, DateTime date);

        Task UpsertMealLogAsync(DailyClientMeals mealLog);

        Task DeleteMealLogAsync(Guid clientId, DateTime date);
    }
}
