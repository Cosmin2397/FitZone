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
            var nutritionData = await _nutritionixRepository.GetFoodsByName(food);
            return Ok(nutritionData);
        }
    }
}
