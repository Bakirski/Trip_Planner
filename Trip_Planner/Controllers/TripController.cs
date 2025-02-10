using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip_Planner.Interfaces;
using Trip_Planner.Models;
using Trip_Planner.Services;

namespace Trip_Planner.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripController : ControllerBase
    {
        private readonly ITrip _tripService;
        public TripController(ITrip tripService)
        {
            _tripService = tripService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Trip>> CreateTrip([FromBody] CreateTripModel createTripModel)
        {
            if(!ModelState.IsValid)
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
                TripName = createTripModel.TripName,
                Destination = createTripModel.Destination,
                Description = createTripModel.Description,
                StartDate = createTripModel.StartDate,
                EndDate = createTripModel.EndDate,
                UserId = parsedUserId
            };

            return await _tripService.CreateTrip(trip);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            return await _tripService.GetTrip(id);
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
        [HttpDelete("{tripId}/destinations/{destinationId}")]
        public async Task<ActionResult> DeleteDestination(int tripId, int destinationId)
        {
            return await _tripService.DeleteDestination(tripId, destinationId);
        }
    }
}
