using System.ComponentModel.DataAnnotations;

namespace FitZone.CalorieTrackerService.Models
{
    public class FoodItem
    {
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Kcal { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Proteins { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Carbs { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Fats { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double MetricPortion { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double NumberOfPortions { get; set; } = 1.0;

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalSizeInGrams => MetricPortion * NumberOfPortions;

        public FoodItem()
        {
            
        }

        public FoodItem(string name, double kcal, double proteins, double fats, double metricPortion)
        {
            Name = name;
            Kcal = kcal;
            Proteins = proteins;
            Fats = fats;
            MetricPortion = metricPortion;
        }

        public void UpdateValuesByPortionSize(double portionSize)
        {
            Kcal = Kcal * portionSize;
            Proteins = Proteins * portionSize;
            Carbs = Carbs * portionSize;
            Fats = Fats * portionSize;
            NumberOfPortions = portionSize;
        }
    }
}
