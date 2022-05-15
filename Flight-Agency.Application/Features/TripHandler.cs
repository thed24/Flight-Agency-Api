using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using LanguageExt;
using Microsoft.Extensions.Logging;

namespace FlightAgency.Application.Features.Trips.TripHandler;

public interface ITripsHandler
{
    Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest);
    List<Trip> GetTrips(int userId);
}

public class TripsHandler : ITripsHandler
{
    private readonly UserContext UserContext;
    private readonly ILogger<TripsHandler> Logger;

    public TripsHandler(UserContext userContext, ILogger<TripsHandler> logger)
    {
        UserContext = userContext;
        Logger = logger;
    }

    public async Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest)
    {
        var user = UserContext.Users.IncludeAllAsync().Single(user => user.Id == userId);

        if (user == null)
        {
            Logger.LogWarning($"User with id {userId} was not found.");
            return Prelude.Left<string, User>("User not found.");
        }

        var stops = createTripRequest.Stops.Select(stop => new Stop()
        {
            Name = stop.Name,
            Location = stop.Location,
            Time = stop.Time,
            Address = stop.Address,
            Category = stop.Category
        }).ToList();

        var trip = new Trip()
        {
            Destination = createTripRequest.Destination,
            Stops = stops
        };

        user.Trips.Add(trip);
        await UserContext.SaveChangesAsync();

        return Prelude.Right<string, User>(user);
    }

    public List<Trip> GetTrips(int userId)
    {
        var user = UserContext.Users.IncludeAllAsync().FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            Logger.LogWarning($"User with id {userId} was not found.");
            return new List<Trip>();
        }

        return user.Trips;
    }
}
