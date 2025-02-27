namespace FitZone.CalorieTrackerService.Models
{
    public class FoodItem
    {
        public string Name { get; set; }

        public double Kcal { get; set; }

        public double Proteins { get; set; }

        public double Carbs { get; set; }

        public double Fats { get; set; }

        public double MetricPortion { get; set; }

        public int NumberOfPortions { get; set; }
    }
}
