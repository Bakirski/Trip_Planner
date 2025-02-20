using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Models.Activities;
using Trip_Planner.Models.Destinations;
using Trip_Planner.Models.Expenses;
using Trip_Planner.Models.Trips;

namespace Trip_Planner.Interfaces
{
    public interface ITripService
    {
        Task<ActionResult<Trip>> CreateTrip(Trip trip);
        Task<ActionResult<Trip>> GetTrip(int id);
        Task<ActionResult<IEnumerable<Trip>>> GetTrips(int userId);
        Task<ActionResult<Trip>> UpdateTrip(int id, UpdateTripModel updateTripModel);
        Task<ActionResult> DeleteTrip(int id);
        Task<ActionResult<Destination>> CreateDestination(Destination destination);
        Task<ActionResult<IEnumerable<Destination>>> GetDestinations(int id);
        Task<ActionResult<Destination>> UpdateDestination(int id, CreateDestinationModel destination);
        Task<ActionResult> DeleteDestination(int tripId, int destinationId);
        Task<ActionResult<Activity>> CreateActivity(Activity activity);
        Task<ActionResult<IEnumerable<Activity>>> GetActivities(int tripId);
        Task<ActionResult> DeleteActivity(int tripId, int activityId);
        Task<ActionResult<Expense>> CreateExpense(Expense expense);
        Task<ActionResult<IEnumerable<Expense>>> GetExpenses(int tripId);
        Task<ActionResult> DeleteExpense(int tripId, int expenseId);
    }
}
