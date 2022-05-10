using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using LanguageExt;

namespace FlightAgency.Application.Features.Trips.TripHandler;

public interface ITripsHandler
{
    Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest);
    Task<List<Trip>> GetTrips(int userId);
}

public class TripsHandler : ITripsHandler
{
    public UserContext UserContext;

    public TripsHandler(UserContext userContext)
    {
        UserContext = userContext;
    }

    public async Task<Either<string, User>> CreateTrip(int userId, CreateTripRequest createTripRequest)
    {
        var stops = createTripRequest.Stops.Select(s => new Stop(0, s.Name, s.Time, s.Address, s.Location, s.Category));
        var trip = new Trip(createTripRequest.Destination, stops);
        var user = (await UserContext.Users.IncludeAllAsync()).FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            Console.WriteLine($"User {userId} was not found.");
            return Prelude.Left<string, User>("User not found.");
        }

        user.Trips.Add(trip);
        UserContext.Users.Update(user);
        await UserContext.SaveChangesAsync();

        return Prelude.Right<string, User>(user);
    }

    public async Task<List<Trip>> GetTrips(int userId)
    {
        var user = (await UserContext.Users.IncludeAllAsync()).FirstOrDefault(user => user.Id == userId);

        if (user is null)
        {
            Console.WriteLine($"User {userId} was not found.");
            return new List<Trip>();
        }

        return user.Trips;
    }
}
