using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string ActivityName { get; set; }

        [Required]
        public int TripId { get; set; }
    }
}
