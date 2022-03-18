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

    [HttpPost(Name = "GetWeatherForecast")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> CreateTrip(
        HttpRequest req,
        int userId,
        ILogger log)
    {
        var createTripRequest = await Mapper.MapStreamToObject<CreateTripRequest>(req.Body);
        if (createTripRequest is null) return new BadRequestResult();

        var trip = await TripService.CreateTrip(userId, createTripRequest);
        return new OkObjectResult(trip);
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> GetTrips(
        HttpRequest req,
        int userId,
        ILogger log)
    {
        var trips = await TripService.GetTrips(userId);
        return new OkObjectResult(trips);
    }

    [HttpPut(Name = "GetWeatherForecast")]
    [Route("{userId}/trips")]
    public async Task<IActionResult> UpdateTrip(
        HttpRequest req,
        int userId,
        ILogger log)
    {
        var updateTripRequest = await Mapper.MapStreamToObject<UpdateTripRequest>(req.Body);
        if (updateTripRequest is null) return new BadRequestResult();

        var trip = await TripService.UpdateTrip(userId, updateTripRequest);
        return new OkObjectResult(trip);
    }
}
