using FlightAgency.Infrastructure;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace FlightAgency.Application.Common;

public static class UserContextExtensions
{
    public static Option<User> FindUserByEmail(this Arr<User> users, string email) =>
        users.Find(user => user.Email == email);

    public static Option<User> FindUserById(this Arr<User> users, int id) =>
        users.Find(user => user.Id == id);

    public static Arr<User> IncludeAll(this DbSet<User> users)
    {
        return users
            .Include(user => user.Trips)
            .ThenInclude(trip => trip.Stops)
            .ThenInclude(stop => stop.Location)
            .Include(user => user.Trips)
            .ThenInclude(trip => trip.Stops)
            .ThenInclude(stop => stop.Time)
            .ToArr();
    }
}
