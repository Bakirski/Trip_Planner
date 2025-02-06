using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip_Planner.Data;
using Trip_Planner.Interfaces;
using Trip_Planner.Models;
namespace Trip_Planner.Services
{
    public class UserService : IUser
    {
        private readonly DatabaseContext _dbContext;
        private readonly JwtService _jwtService;
        public UserService(DatabaseContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }


        public async Task<ActionResult<User>> RegisterUser(UserRegistrationModel model)
        {
            var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == model.Username || u.UserEmail == model.Email);

            if (existingUser != null)
            {
                return new BadRequestObjectResult("User already exists. Please log in.");
            }

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Username = model.Username,
                UserEmail = model.Email,
            };

            user.Password = hasher.HashPassword(user, model.Password);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new OkObjectResult(user);
        }
        public async Task<IActionResult> AuthenticateUser(UserLoginModel model)
        {
            if (model == null)
            {
                return new BadRequestObjectResult("Incomplete data provided.");
            }

            // Fetch user from database (example using Entity Framework)
            var user = await _dbContext.Users
                .Where(u => u.UserEmail == model.UserEmail)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new BadRequestObjectResult("User is not registered. Please Sign Up.");
            }

            // Compare provided password with stored hashed password
            var hasher = new PasswordHasher<User>();
            var isPasswordValid = hasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (isPasswordValid != PasswordVerificationResult.Success)
            {
                return new UnauthorizedObjectResult("Invalid credentials.");
            }
            var token = _jwtService.GenerateJwtToken(user);
            return new OkObjectResult(new { Token = token});

        }

        public async Task<ActionResult<User>> GetUser(int Id)
        {
            var user = await _dbContext.Users.FindAsync(Id);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found.");
            }

            return user;
        }
    }
}
