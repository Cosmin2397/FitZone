using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitZone.Client.Shared.DTOs.CalorieTracker
{
    public class DailyClientMealsDto
    {
        public string Id { get; set; } = string.Empty;

        public Guid ClientId { get; set; }

        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$", ErrorMessage = "Data trebuie să fie în formatul dd-MM-yyyy")]
        public string Date { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public string PtComment { get; set; } = string.Empty;

    }

    public class Meal
    {
        public MealType Name { get; set; }

        public List<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MealType
    {
        Breakfast = 0,
        Lunch = 1,
        Dinner = 2,
        Snack = 3
    }
}
