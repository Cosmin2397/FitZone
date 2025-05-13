using FitZone.Client.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface ICalorieTrackerService
    {
        public Task<FoodItem> GetFoodNutrition(string food);

        public Task<FoodItem> UpdateQuantity(FoodItem food, double newQuantity);

        public Task<DailyClientMealsDto> GetDailyLog(string date, Guid clientId);

        public Task<DailyClientMealsDto> UpdateDailyLog(DailyClientMealsDto meal);

        public Task<bool> DeleteDailyLog(Guid clientId, string date);
    }
}
