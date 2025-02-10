using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Activities
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
