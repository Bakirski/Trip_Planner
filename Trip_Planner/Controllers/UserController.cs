using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Interfaces;
using Trip_Planner.Models;
using Trip_Planner.Services;

namespace Trip_Planner.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUser _service;
        public UserController(IUser service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<User>> GetUser()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from JWT

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return BadRequest("Invalid user ID.");
            }

            var user = await _service.GetUser(userId); // Pass the userId to the service

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register(UserRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterUser(model);

                if (result.Result is BadRequestObjectResult badRequest)
                {
                    return badRequest;

                }
                return Ok(new { message = "Registration successful! Please log in." });

            }
            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

            return BadRequest(new { message = "Invalid registration data." });

        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> Authenticate(UserLoginModel model)
        {
            return await _service.AuthenticateUser(model);
        }

    }
}
