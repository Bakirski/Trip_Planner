using Newtonsoft.Json;

namespace Trip_Planner.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string TripName { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int UserId { get; set; }
    }
}
