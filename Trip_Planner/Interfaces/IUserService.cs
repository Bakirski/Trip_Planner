using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Models.Users;
namespace Trip_Planner.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<User>> RegisterUser(UserRegistrationModel model);
        Task<IActionResult> AuthenticateUser(UserLoginModel model);
        Task<ActionResult<User>> GetUser(int Id);
    }
}
