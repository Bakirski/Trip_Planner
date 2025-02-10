using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Models;

namespace Trip_Planner.Interfaces
{
    public interface ITrip
    {
        Task<ActionResult<Trip>> CreateTrip(Trip trip);
        Task<ActionResult<Trip>> GetTrip(int id);
        Task<ActionResult<Trip>> UpdateTrip(int id, UpdateTripModel updateTripModel);
        Task<ActionResult> DeleteTrip(int id);
        Task<ActionResult<Destination>> CreateDestination(Destination destination);
        Task<ActionResult<IEnumerable<Destination>>> GetDestinations(int id);
        Task<ActionResult> DeleteDestination(int tripId, int destinationId);
        
    }
}
