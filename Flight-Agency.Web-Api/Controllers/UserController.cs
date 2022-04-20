using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/users")]
public class UserController
{
    public ITripsHandler TripsHandler { get; }
    public UserController(ITripsHandler tripsHandler)
    {
        TripsHandler = tripsHandler;
    }

    [HttpPost("{userId}/trips")]
    public async Task<IActionResult> CreateTrip([FromRoute] int userId, [FromBody] CreateTripRequest createTripRequest)
    {
        return (await TripsHandler
            .CreateTrip(userId, createTripRequest))
            .MapToApiResponse<string, User>();
    }

    [HttpGet("{userId}/trips")]
    public async Task<IActionResult> GetTrips([FromRoute] int userId)
    {
        return new OkObjectResult(await TripsHandler
            .GetTrips(userId));
    }
}