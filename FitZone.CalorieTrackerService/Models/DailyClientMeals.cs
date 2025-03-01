using FitZone.CalorieTrackerService.Models.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitZone.CalorieTrackerService.Models
{
    public class DailyClientMeals
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid ClientId { get; set; }

        public DateTime Date { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public string PtComment { get; set; }


        public DailyClientMeals()
        {
            
        }

        public DailyClientMeals(Guid clientId)
        {
            ClientId = clientId;
            Date = DateTime.Now;
        }

        public void AddFoodToMeal(MealType meal, FoodItem item)
        {
            var existingMeal = Meals.FirstOrDefault(m => m.Name == meal);
            if (existingMeal == null)
            {
                Meal newMeal = new Meal(meal, item);
            }
            else
            {
                existingMeal.AddItem(item);
            }
        }

        public void RemoveFoodFromMeal(MealType meal, FoodItem item)
        {
            var existingMeal = Meals.FirstOrDefault(m => m.Name == meal);
            if (existingMeal != null)
            {
                existingMeal.RemoveItem(item);
            }
        }
    }
}
