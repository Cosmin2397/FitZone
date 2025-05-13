using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using FitZone.CalorieTrackerService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.CalorieTrackerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionixService _nutritionixService;

        public NutritionController(INutritionixService nutritionixService)
        {
            _nutritionixService = nutritionixService;
        }

        [HttpGet("{food}")]
        public async Task<IActionResult> GetFoodNutrition(string food)
        {
            var nutritionData = await _nutritionixService.GetFoodByName(food);
            if (nutritionData != null)
            {
                return Ok(nutritionData);
            }

            return NotFound();
        }

        [HttpPut("updatequantity/{newQuantity}")]
        public IActionResult GetFoodNutrition([FromBody] FoodItem food, [FromRoute] double newQuantity)
        {
            var nutritionData = _nutritionixService.UpdateFoodSize(food, newQuantity);
            if (nutritionData != null)
            {
                return Ok(nutritionData);
            }

            return NotFound();
        }
    }
}
