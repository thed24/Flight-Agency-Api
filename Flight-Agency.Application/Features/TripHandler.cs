using FlightAgency.Application.Common;
using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Domain;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

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
        var users = (await UserContext.Users.ToListAsync()).ToFSharpList();
        var trip = new Trip() { Destination = createTripRequest.Destination, Stops = createTripRequest.Stops };
        var result = UserAggregateRoot.AddTrip(userId, users, trip);

        if (result.IsOk)
        {
            var user = result.ResultValue;
            await UserContext.Users.AddAsync(user);
            await UserContext.SaveChangesAsync();
        }

        return result.ToEither();
    }

    public async Task<List<Trip>> GetTrips(int userId)
    {
        var users = (await UserContext.Users.ToListAsync()).ToFSharpList();
        return UserAggregateRoot.GetTrips(userId, users).ToList();
    }
}
