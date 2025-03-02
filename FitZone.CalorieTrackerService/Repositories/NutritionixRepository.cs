using FitZone.CalorieTrackerService.Configurations;
using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FitZone.CalorieTrackerService.Repositories
{
    public class NutritionixRepository: INutritionixRepository
    {
        private readonly HttpClient _httpClient;
        private readonly NutritionixSettings _settings;

        public NutritionixRepository(HttpClient httpClient, IOptions<NutritionixSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<FoodItem> GetFoodByName(string food)
        {
            var requestBody = new { query = food };
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("x-app-id", _settings.AppId);
            _httpClient.DefaultRequestHeaders.Add("x-app-key", _settings.AppKey);

            var response = await _httpClient.PostAsync(_settings.BaseUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseObj = JObject.Parse(responseString);
                var foodItem = responseObj["foods"]?.FirstOrDefault();
                FoodItem item;
                if (foodItem != null)
                {
                    item = new FoodItem
                    {
                        Name = foodItem["food_name"]?.ToString(),
                        Kcal = foodItem["nf_calories"]?.ToObject<double>() ?? 0,
                        Proteins = foodItem["nf_protein"]?.ToObject<double>() ?? 0,
                        Carbs = foodItem["nf_total_carbohydrate"]?.ToObject<double>() ?? 0,
                        Fats = foodItem["nf_total_fat"]?.ToObject<double>() ?? 0,
                        MetricPortion = foodItem["serving_weight_grams"]?.ToObject<double>() ?? 0
                    };

                    return Set100GramsPortion(item);
                }
            }
            return null;
        }


        public FoodItem UpdateFoodSize(FoodItem item, double newPortionsSize)
        {
            if (item != null && newPortionsSize > 0.0)
            {
                item.UpdateValuesByPortionSize(newPortionsSize);
                return item;
            }

            return null;
        }


        private FoodItem Set100GramsPortion(FoodItem food)
        {
            var convertedFoodItem = new FoodItem();
            if (food.MetricPortion > 0.0)
            {
                double factorConversie = 100.0 / food.MetricPortion;
                convertedFoodItem.Name = food.Name;
                convertedFoodItem.MetricPortion = 100.0;
                convertedFoodItem.Kcal = food.Kcal * factorConversie;
                convertedFoodItem.Fats = food.Fats * factorConversie;
                convertedFoodItem.Proteins = food.Proteins * factorConversie;
                convertedFoodItem.Carbs = food.Carbs * factorConversie;
                convertedFoodItem.NumberOfPortions = food.NumberOfPortions;
            }

            return convertedFoodItem;
        }
    }
}
