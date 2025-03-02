using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitZone.CalorieTrackerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{date}/{clientId}")]
        public async Task<IActionResult> GetFoodNutrition(string date, Guid clientId)
        {
            var meals = await _mealService.GetMealsAsync(clientId,date);
            if (meals != null)
            {
                return Ok(meals);
            }

            return NotFound();
        }

        [HttpPut("/updateMeal")]
        public async Task<IActionResult> UpdateDailyLog(DailyClientMeals meal)
        {
            await _mealService.UpsertMealLogAsync(meal);
            var updatedMeals = await _mealService.GetMealsAsync(meal.ClientId, meal.Date);
            if (updatedMeals != null)
            {
                return Ok(updatedMeals);
            }

            return NotFound();
        }

        [HttpDelete("/deleteMeal/{clientId}/{date}")]
        public async Task<IActionResult> DeleteDailyLog(Guid clientId, string date)
        {
            await _mealService.DeleteMealLogAsync(clientId,date);
            var deletedMeals = await _mealService.GetMealsAsync(clientId, date);
            if (deletedMeals == null)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
