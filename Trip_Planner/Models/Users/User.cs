using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trip_Planner.Models.Trips;

namespace Trip_Planner.Models.Users
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

        [JsonIgnore]
        public ICollection<Trip> Trips { get; set; }
    }
}
