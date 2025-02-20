using Trip_Planner.Models.Destinations;
using Trip_Planner.Models.Trips;

namespace Trip_Planner.Models
{
    public class CreateTripWithDestinationModel
    {
        public CreateTripModel Trip { get; set; }
        public CreateDestinationModel Destination { get; set; }
    }
}
