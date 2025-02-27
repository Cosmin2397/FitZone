using FitZone.CalorieTrackerService.Models.Enum;

namespace FitZone.CalorieTrackerService.Models
{
    public class Meal
    {
        public MealType Name { get; set; }

        public List<FoodItem> FoodItems = new List<FoodItem>();

        public Meal()
        {
            
        }

        public Meal(MealType type, List<FoodItem> items)
        {
            Name = type;
            FoodItems = items;
        }

        public Meal(MealType type, FoodItem item)
        {
            Name = type;
            FoodItems.Add(item);
        }

        public void AddItem(FoodItem item)
        {
            FoodItems.Add(item);
        }

        public void RemoveItem(FoodItem item)
        {
            FoodItems.Remove(item);
        }
    }
}
