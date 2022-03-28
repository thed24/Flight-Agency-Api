using FlightAgency.Application.Common;
using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Infrastructure;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightAgency.Application.Features.Trips.TripHandler;

public interface ITripsHandler
{
    Either<string, Trip> CreateTrip(int userId, CreateTripRequest createTripRequest);
    Either<string, Trip> UpdateTrip(UpdateTripRequest updateTripRequest);
    Either<string, List<Trip>> GetTrips(int userId);
}

public class TripsHandler : ITripsHandler
{
    public UserContext UserContext;

    public TripsHandler(UserContext userContext)
    {
        UserContext = userContext;
    }

    public Either<string, Trip> CreateTrip(int userId, CreateTripRequest request) =>
        UserContext
            .Users
            .IncludeAll()
            .FindUserById(userId)
            .Match<Either<string, User>>(None: () => "User not found.",
                                         Some: (user) =>
                                         {
                                             user.Trips.Add(new Trip(request.Destination, request.Stops));
                                             return user;
                                         })
            .Match<Either<string, User>>(Left: (error) => error,
                                         Right: (user) => PersistUser(user, UserContext.Users.Update))
            .Match<Either<string, Trip>>(Left: (error) => error,
                                         Right: (user) => user.Trips.Last());

    public Either<string, List<Trip>> GetTrips(int userId) =>
        UserContext
            .Users
            .IncludeAll()
            .FindUserById(userId)
            .Match<Either<string, List<Trip>>>(None: () => "User not found.",
                                               Some: (user) => user.Trips);

    public Either<string, Trip> UpdateTrip(UpdateTripRequest request) =>
        UserContext
            .Trips
            .Find(trip => trip.Id == request.Id)
            .Match<Either<string, Trip>>(None: () => "User not found.",
                                         Some: (_) => new Trip(request.Name, request.Stops, request.Id))
            .Match<Either<string, Trip>>(Left: (error) => error,
                                         Right: (trip) => PersistTrip(trip, UserContext.Trips.Update));

    public Trip PersistTrip(Trip trip, Func<Trip, EntityEntry<Trip>> sideEffectFunc)
    {
        sideEffectFunc(trip);
        UserContext.SaveChanges();
        return trip;
    }

    public User PersistUser(User user, Func<User, EntityEntry<User>> sideEffectFunc)
    {
        sideEffectFunc(user);
        UserContext.SaveChanges();
        return user;
    }
}
