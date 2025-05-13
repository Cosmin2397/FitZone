using FitZone.Client.Shared.DTOs;
using FitZone.Client.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitZone.Client.Shared.Services
{
    public class CalorieTrackerService : ICalorieTrackerService
    {
        private readonly HttpClient _httpClient;

        public CalorieTrackerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FoodItem> GetFoodNutrition(string food)
        {
            try
            {
                string url = ($"/caloriesService/Nutrition/{food}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var apiFood = await response.Content.ReadFromJsonAsync<FoodItem>();
                    return apiFood;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<FoodItem> UpdateQuantity(FoodItem food, double newQuantity)
        {
            try
            {
                string url = ($"/caloriesService/Nutrition/updatequantity/{newQuantity}");
                var response = await _httpClient.PutAsJsonAsync(url, food);

                if (response.IsSuccessStatusCode)
                {
                    var apiFood = await response.Content.ReadFromJsonAsync<FoodItem>();
                    return apiFood;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        public async Task<bool> DeleteDailyLog(Guid clientId, string date)
        {
            try
            {
                string url = ($"/caloriesService/Meal/deleteMeal/{clientId}/{date}");
                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<DailyClientMealsDto> GetDailyLog(string date, Guid clientId)
        {
            try
            {
                string url = ($"/caloriesService/Meal/{date}/{clientId}");
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                    };
                }
                    var dailyMeals = await response.Content.ReadFromJsonAsync<DailyClientMealsDto>();
                return dailyMeals;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<DailyClientMealsDto> UpdateDailyLog(DailyClientMealsDto meal)
        {
            try
            {
                string url = ($"/caloriesService/Meal/updateMeal");
                var response = await _httpClient.PutAsJsonAsync(url, meal);

                if (response.IsSuccessStatusCode)
                {
                    var dailyMeals = await GetDailyLog(meal.Date, meal.ClientId);
                    return dailyMeals;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
