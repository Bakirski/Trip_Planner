using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Interfaces;
using Trip_Planner.Models;
using Trip_Planner.Models.Activities;
using Trip_Planner.Models.Destinations;
using Trip_Planner.Models.Expenses;
using Trip_Planner.Models.Trips;
using Trip_Planner.Services;
using Activity = Trip_Planner.Models.Activities.Activity;

namespace Trip_Planner.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly ILogger<TripController> _logger;
        public TripController(ITripService tripService, ILogger<TripController> logger)
        {
            _tripService = tripService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Trip>> CreateTrip([FromBody] CreateTripWithDestinationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null || model.Trip == null || model.Destination == null)
            {
                return BadRequest("Invalid data provided.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }
            int parsedUserId = int.Parse(userId);

            var trip = new Trip
            {
                TripName = model.Trip.TripName,
                Description = model.Trip.Description,
                StartDate = model.Trip.StartDate,
                EndDate = model.Trip.EndDate,
                UserId = parsedUserId
            };

            var tripResult = await _tripService.CreateTrip(trip);

            var destination = new Destination
            {
                DestinationName = model.Destination.DestinationName,
                TripId = tripResult.Id
            };

            var destinationResult = await _tripService.CreateDestination(destination);

            return Ok(new { Trip = tripResult, Destination = destinationResult });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            return await _tripService.GetTrip(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }
            int parsedUserId = int.Parse(userId);
            return await _tripService.GetTrips(parsedUserId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Trip>> UpdateTrip(int id, [FromBody] UpdateTripModel updateTripModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }
            return await _tripService.UpdateTrip(id, updateTripModel);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrip(int id)
        {
            return await _tripService.DeleteTrip(id);
        }

        [Authorize]
        [HttpPost("{id}/destinations")]
        public async Task<ActionResult<Destination>> CreateDestination(int id, [FromBody] CreateDestinationModel createDestinationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }
            var destination = new Destination
            {
                DestinationName = createDestinationModel.DestinationName,
                TripId = id
            };
            return await _tripService.CreateDestination(destination);
        }

        [Authorize]
        [HttpGet("{id}/destinations")]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations(int id)
        {
            return await _tripService.GetDestinations(id);
        }

        [Authorize]
        [HttpPatch("{id}/destinations/")]
        public async Task<ActionResult<Destination>> UpdateDestination(int id, [FromBody] CreateDestinationModel updateDestinationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }
            return await _tripService.UpdateDestination(id, updateDestinationModel);
        }

        [Authorize]
        [HttpDelete("{tripId}/destinations/{destinationId}")]
        public async Task<ActionResult> DeleteDestination(int tripId, int destinationId)
        {
            return await _tripService.DeleteDestination(tripId, destinationId);
        }

        [Authorize]
        [HttpPost("{id}/activities")]
        public async Task<ActionResult<Activity>> CreateActivity(int id, [FromBody] CreateActivityModel createActivityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }
            var activity = new Activity
            {
                ActivityName = createActivityModel.ActivityName,
                TripId = id
            };
            return await _tripService.CreateActivity(activity);
        }

        [Authorize]
        [HttpGet("{id}/activities")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities(int id)
        {
            return await _tripService.GetActivities(id);
        }

        [Authorize]
        [HttpDelete("{tripId}/activities/{activityId}")]
        public async Task<ActionResult> DeleteActivity(int tripId, int activityId)
        {
            return await _tripService.DeleteActivity(tripId, activityId);
        }

        [Authorize]
        [HttpPost("{id}/expenses")]
        public async Task<ActionResult<Expense>> CreateExpense(int id, [FromBody] CreateExpenseModel createExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }
            var expense = new Expense
            {
                expenseName = createExpenseModel.ExpenseName,
                expenseAmount = createExpenseModel.ExpenseAmount,
                TripId = id
            };
            return await _tripService.CreateExpense(expense);
        }

        [Authorize]
        [HttpGet("{id}/expenses")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(int id)
        {
            return await _tripService.GetExpenses(id);
        }

        [Authorize]
        [HttpDelete("{tripId}/expenses/{expenseId}")]
        public async Task<ActionResult> DeleteExpense(int tripId, int expenseId)
        {
            return await _tripService.DeleteExpense(tripId, expenseId);
        }
    }
}
