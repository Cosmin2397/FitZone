using FitZone.CalorieTrackerService.Models.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FitZone.CalorieTrackerService.Models
{
    public class DailyClientMealsDto
    {
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid ClientId { get; set; }

        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$", ErrorMessage = "Data trebuie să fie în formatul dd-MM-yyyy")]
        public string Date { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public string PtComment { get; set; }
    }
}
