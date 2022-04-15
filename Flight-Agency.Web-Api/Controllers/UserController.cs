using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.Models;
using Microsoft.AspNetCore.Mvc;

public class UserController
{
    public ITripsHandler TripsHandler { get; }
    public UserController(ITripsHandler tripsHandler)
    {
        TripsHandler = tripsHandler;
    }

    [HttpPost("api/users/{userId}/trips")]
    public async Task<IResult> CreateTrip(int userId, CreateTripRequest createTripRequest)
    {
        return (await TripsHandler.CreateTrip(userId, createTripRequest)).MapToApiResponse<string, User>();
    }

    [HttpGet("api/users/{userId}/trips")]
    public async Task<IResult> GetTrips(int userId)
    {
        return Results.Ok(await TripsHandler.GetTrips(userId));
    }
}