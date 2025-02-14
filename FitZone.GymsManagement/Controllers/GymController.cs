using FitZone.GymsManagement.Dtos;
using FitZone.GymsManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.GymsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly IGymService gymService;

        public GymController(IGymService gymService)
        {
            this.gymService = gymService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymDTO>>> GetGyms()
        {
            return await gymService.GetGyms();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymDTO>> GetGymById(Guid id)
        {
            var gym = await gymService.GetGymById(id);

            if (gym == null)
            {
                return NotFound();
            }

            return gym;
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutGym(Guid id, GymDTO gymUpdated)
        {
            GymDTO updatedGym;
            try
            {
                 updatedGym = await gymService.UpdateGym(gymUpdated, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(updatedGym);
        }

        [HttpPost]
        public async Task<ActionResult<GymDTO>> PostGym(GymDTO gym)
        {
            try
            {
                return await gymService.AddGym(gym);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteGym(Guid id)
        {
            try
            {
                return await gymService.RemoveGym(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

