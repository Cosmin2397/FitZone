using System.Threading.Tasks;
using FitZone.CalorieTrackerService.Models;
using Newtonsoft.Json.Linq;

namespace FitZone.CalorieTrackerService.Repositories.Interfaces
{


    public interface INutritionixRepository
    {
        Task<List<FoodItem>> GetFoodsByName(string food);
    }

}
