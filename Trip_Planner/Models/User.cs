using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
