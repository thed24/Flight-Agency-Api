using FlightAgency.Models;
using FlightAgency.Models.User;
using Microsoft.EntityFrameworkCore;

namespace FlightAgency.Infrastructure;

public static class UserContextExtensions
{
    public static IQueryable<User> IncludeAllAsync(this DbSet<User> users)
    {
        return users
            .Include(u => u.Trips)
            .ThenInclude(t => t.Stops)
            .ThenInclude(s => s.Time)
            .Include(u => u.Trips)
            .ThenInclude(t => t.Stops)
            .ThenInclude(s => s.Location)
            .AsQueryable();
    }
}