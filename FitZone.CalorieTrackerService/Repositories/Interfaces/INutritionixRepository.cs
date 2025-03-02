using System.Threading.Tasks;
using FitZone.CalorieTrackerService.Models;
using Newtonsoft.Json.Linq;

namespace FitZone.CalorieTrackerService.Repositories.Interfaces
{


    public interface INutritionixRepository
    {
        Task<FoodItem> GetFoodByName(string food);

        FoodItem UpdateFoodSize(FoodItem item, double newPortionsSize);
    }

}
