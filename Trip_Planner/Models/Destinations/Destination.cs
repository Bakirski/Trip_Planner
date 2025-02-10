using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Destinations
{
    public class Destination
    {
        public int Id { get; set; }

        [Required]
        public string DestinationName { get; set; }

        [Required]
        public int TripId { get; set; }

    }
}
