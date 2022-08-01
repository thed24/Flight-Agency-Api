using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController
{
    private ITripsHandler TripsHandler { get; }
    public UserController(ITripsHandler tripsHandler)
    {
        TripsHandler = tripsHandler;
    }

    [HttpPost("{userId}/trips")]
    public async Task<IActionResult> CreateTrip([FromRoute] int userId, [FromBody] CreateTripRequest createTripRequest)
    {
        return (await TripsHandler
            .CreateTrip(userId, createTripRequest))
            .MapToApiResponse();
    }

    [HttpGet("{userId}/trips")]
    public IActionResult GetTrips([FromRoute] int userId)
    {
        return new OkObjectResult(TripsHandler.GetTrips(userId));
    }
}