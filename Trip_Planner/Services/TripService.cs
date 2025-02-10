using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trip_Planner.Data;
using Trip_Planner.Interfaces;
using Trip_Planner.Models.Activities;
using Trip_Planner.Models.Destinations;
using Trip_Planner.Models.Expenses;
using Trip_Planner.Models.Trips;

namespace Trip_Planner.Services
{
    public class TripService : ITripService
    {
        private readonly DatabaseContext _dbContext;

        public TripService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<Trip>> CreateTrip(Trip trip)
        {
            _dbContext.Trips.Add(trip);
            await _dbContext.SaveChangesAsync();
            var destination = new Destination
            {
                DestinationName = trip.Destination,
                TripId = trip.Id
            };

            _dbContext.Destinations.Add(destination);

            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(trip);
        }

        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            var trip = await _dbContext.Trips.FindAsync(id);
            if (trip == null)
            {
                return new NotFoundObjectResult("Could not find a trip that matches the id.");
            }
            trip.StartDate = trip.StartDate;
            trip.EndDate = trip.EndDate;
            return new OkObjectResult(trip);
        }

        public async Task<ActionResult<Trip>> UpdateTrip(int id, UpdateTripModel trip)
        {
            var existingTrip = await _dbContext.Trips.FindAsync(id);
            if (existingTrip == null)
            {
                return new NotFoundObjectResult("Could not find trip to update.");
            }
            existingTrip.TripName = trip.TripName;
            existingTrip.Destination = trip.Destination;
            existingTrip.Description = trip.Description;
            existingTrip.StartDate = trip.StartDate;
            existingTrip.EndDate = trip.EndDate;
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(existingTrip);
        }

        public async Task<ActionResult> DeleteTrip(int id)
        {
            var trip = await _dbContext.Trips.FindAsync(id);
            if (trip == null)
            {
                return new NotFoundObjectResult("Could not find trip to delete.");
            }
            _dbContext.Trips.Remove(trip);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Destination>> CreateDestination(Destination destination)
        {
            _dbContext.Destinations.Add(destination);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(destination);
        }

        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations(int id)
        {
            var destinations = await _dbContext.Destinations.Where(d => d.TripId == id).ToListAsync();

            if (destinations.Count == 0)
            {
                return new NotFoundObjectResult("Could not find any destinations for this trip.");
            }

            return new OkObjectResult(destinations);
        }

        public async Task<ActionResult> DeleteDestination(int tripId, int destinationId)
        {
            var destination = await _dbContext.Destinations.FindAsync(destinationId);
            if (destination == null)
            {
                return new NotFoundObjectResult("Could not find destination to delete.");
            }
            _dbContext.Destinations.Remove(destination);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
        {
            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(activity);
        }

        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities(int tripId)
        {
            var activities = await _dbContext.Activities.Where(a => a.TripId == tripId).ToListAsync();

            if (activities.Count == 0)
            {
                return new NotFoundObjectResult("Could not find any activities for this trip.");
            }

            return new OkObjectResult(activities);
        }

        public async Task<ActionResult> DeleteActivity(int tripId, int activityId)
        {
            var activity = await _dbContext.Activities.FindAsync(activityId);
            if (activity == null)
            {
                return new NotFoundObjectResult("Could not find activity to delete.");
            }
            _dbContext.Activities.Remove(activity);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(expense);
        }

        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(int tripId)
        {
            var expenses = await _dbContext.Expenses.Where(e => e.TripId == tripId).ToListAsync();

            if (expenses.Count == 0)
            {
                return new NotFoundObjectResult("Could not find any expenses for this trip.");
            }

            return new OkObjectResult(expenses);
        }

        public async Task<ActionResult> DeleteExpense(int tripId, int expenseId)
        {
            var expense = await _dbContext.Expenses.FindAsync(expenseId);
            if (expense == null)
            {
                return new NotFoundObjectResult("Could not find expense to delete.");
            }
            _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }

    }
}
