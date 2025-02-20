using System.Text.Json.Serialization;
using Trip_Planner.Models.Activities;
using Trip_Planner.Models.Destinations;
using Trip_Planner.Models.Expenses;

namespace Trip_Planner.Models.Trips
{
    public class Trip
    {
        public int Id { get; set; }
        public string TripName { get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int UserId { get; set; }

        public ICollection<Destination> Destinations { get; set; }

        [JsonIgnore]
        public ICollection<Activity> Activities { get; set; }

        [JsonIgnore]
        public ICollection<Expense> Expenses { get; set; }
    }
}
