using Microsoft.AspNetCore.Mvc;
using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Domain;
using Flight_Agency_Infrastructure.Common;

namespace Flight_Agency_Api;

[ApiController]
public class Trips : ControllerBase
{
    private ITripsService TripService { get; }

    public Trips(ITripsService tripService)
    {
        TripService = tripService;
    }

    [HttpPost(Name = "CreateTrip")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> CreateTrip(
        [FromBody] CreateTripRequest createTripRequest,
        [FromRoute] int userId)
    {
        var trip = await TripService.CreateTrip(userId, createTripRequest);
        return new OkObjectResult(trip);
    }

    [HttpGet(Name = "GetTrips")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> GetTrips(
        [FromBody] CreateTripRequest createTripRequest,
        [FromRoute] int userId)
    {
        var trips = await TripService.GetTrips(userId);
        return new OkObjectResult(trips);
    }

    [HttpPut(Name = "UpdateTrip")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> UpdateTrip(
        [FromBody] UpdateTripRequest updateTripRequest,
        [FromRoute] int userId)
    {
        var trip = await TripService.UpdateTrip(userId, updateTripRequest);
        return new OkObjectResult(trip);
    }
}
