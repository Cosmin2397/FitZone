using FitZone.CalorieTrackerService.Models;
using FitZone.CalorieTrackerService.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.CalorieTrackerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionixRepository _nutritionixRepository;

        public NutritionController(INutritionixRepository nutritionixRepository)
        {
            _nutritionixRepository = nutritionixRepository;
        }

        [HttpGet("{food}")]
        public async Task<IActionResult> GetFoodNutrition(string food)
        {
            var nutritionData = await _nutritionixRepository.GetFoodByName(food);
            if (nutritionData != null)
            {
                return Ok(nutritionData);
            }

            return NotFound();
        }

        [HttpPut("/updatequantity/{newQuantity}")]
        public IActionResult GetFoodNutrition(FoodItem food, double newQuantity)
        {
            var nutritionData =  _nutritionixRepository.UpdateFoodSize(food, newQuantity);
            if (nutritionData != null)
            {
                return Ok(nutritionData);
            }

            return NotFound();
        }
    }
}
