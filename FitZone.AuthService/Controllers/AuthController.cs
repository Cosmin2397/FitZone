using FitZone.AuthService.Dtos;
using FitZone.AuthService.Entities;
using FitZone.AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentificationService authService;

        public AuthController(IAuthentificationService authService)
        {
            this.authService = authService;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            var result = await authService.RegisterUser(user);
            if (result)
            {
                return Ok("Registration made successful!");
            }

            return BadRequest("Error occurred!");
        }

        [HttpPost]
        [Route("logout/{email}")]
        public async Task<IActionResult> Logout(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var success = await authService.LogoutUser(email);
            if (success)
            {
                return Ok("Logout successful.");
            }
            else
            {
                return StatusCode(500, "Error logging out.");
            }
        }


        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            LoginResponse response = await authService.LoginUser(user);
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest("Error occurred!");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            var loginResult = await authService.RefreshToken(model);
            if (loginResult != null)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }

        [HttpGet("Users")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await authService.GetAllUsers();
            if (users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await authService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(ApplicationUser updatedUser)
        {
            try
            {
                await authService.UpdateUser(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<bool>> DeleteUser(string email)
        {
            try
            {
                return await authService.DeleteUser(email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
