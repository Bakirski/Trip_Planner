using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Users
{
    public class UserLoginModel
    {

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
