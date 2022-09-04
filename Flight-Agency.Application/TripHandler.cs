using FlightAgency.Contracts.Requests.Trips;
using FlightAgency.Infrastructure;
using FlightAgency.Models.User;
using LanguageExt;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace FlightAgency.Application;

public interface ITripsHandler
{
    Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest);
    List<Trip> GetTrips(int userId);
}

public class TripsHandler : ITripsHandler
{
    private readonly ILogger<TripsHandler> Logger;
    private readonly UserContext UserContext;

    public TripsHandler(UserContext userContext, ILogger<TripsHandler> logger)
    {
        UserContext = userContext;
        Logger = logger;
    }

    public async Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest)
    {
        User? user = UserContext.Users.IncludeAllAsync().Single(user => user.Id == userId);

        if (user is null)
        {
            Logger.LogError($"User with id {userId} was not found.");
            return Left<string, User>("User not found.");
        }

        List<Stop> stops = createTripRequest.Stops.Select(stop => new Stop
        {
            Name = stop.Name,
            Location = stop.Location,
            Time = stop.Time,
            Address = stop.Address,
            Category = stop.Category
        }).ToList();

        Trip trip = new()
        {
            Destination = createTripRequest.Destination,
            Stops = stops
        };

        user.Trips.Add(trip);
        await UserContext.SaveChangesAsync();

        return Right<string, User>(user);
    }

    public List<Trip> GetTrips(int userId)
    {
        User? user = UserContext.Users.IncludeAllAsync().FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            Logger.LogError($"User with id {userId} was not found.");
            return new List<Trip>();
        }

        return user.Trips;
    }
}