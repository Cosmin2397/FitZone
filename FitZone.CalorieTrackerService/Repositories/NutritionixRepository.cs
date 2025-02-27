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

        public async Task<List<FoodItem>> GetFoodsByName(string food)
        {
            var requestBody = new { query = food };
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("x-app-id", _settings.AppId);
            _httpClient.DefaultRequestHeaders.Add("x-app-key", _settings.AppKey);

            var response = await _httpClient.PostAsync(_settings.BaseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj =  JObject.Parse(responseString);

            return new List<FoodItem>();
        }
    }
}
