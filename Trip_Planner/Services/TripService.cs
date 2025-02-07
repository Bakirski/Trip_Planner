using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Data;
using Trip_Planner.Interfaces;
using Trip_Planner.Models;

namespace Trip_Planner.Services
{
    public class TripService : ITrip
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
    }
}
