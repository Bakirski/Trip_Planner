using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models
{
    public class UserLoginModel
    {

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
