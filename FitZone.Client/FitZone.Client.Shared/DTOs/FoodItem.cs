using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
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
    }
}
