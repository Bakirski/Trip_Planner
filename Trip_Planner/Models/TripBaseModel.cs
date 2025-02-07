using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models
{
    public class TripBaseModel
    {
        [Required]
        public string TripName { get; set; }

        [Required]
        public string Destination { get; set; }

        public string Description { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }
    }
}
