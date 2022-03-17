using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Infrastructure.Common;
using Flight_Agency_Domain;

namespace Flight_Agency_Api;

public class Trips
{
    private ITripsService TripService { get; }

    public Trips(ITripsService tripService)
    {
        TripService = tripService;
    }

    [FunctionName(nameof(CreateTrip))]
    public async Task<IActionResult> CreateTrip(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "{userId}/trips")]
        HttpRequest req,
        int userId,
        ILogger log)
    {
        var createTripRequest = await Mapper.MapStreamToObject<CreateTripRequest>(req.Body);
        if (createTripRequest is null) return new BadRequestResult();

        var trip = await TripService.CreateTrip(userId, createTripRequest);
        return new OkObjectResult(trip);
    }

    [FunctionName(nameof(GetTrips))]
    public async Task<IActionResult> GetTrips(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{userId}/trips")]
        HttpRequest req,
        int userId,
        ILogger log)
    {
        var trips = await TripService.GetTrips(userId);
        return new OkObjectResult(trips);
    }

    [FunctionName(nameof(UpdateTrip))]
    public async Task<IActionResult> UpdateTrip(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "{userId}/trips")]
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
