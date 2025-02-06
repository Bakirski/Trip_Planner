using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Models;
namespace Trip_Planner.Interfaces
{
    public interface IUser
    {
        Task<ActionResult<User>> RegisterUser(UserRegistrationModel model);
        Task<IActionResult> AuthenticateUser(UserLoginModel model);
        Task<ActionResult<User>> GetUser(int Id);
    }
}
