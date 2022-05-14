using FlightAgency.Models;
using Microsoft.EntityFrameworkCore;

public static class UserContextExtensions
{
    public static IQueryable<User> IncludeAllAsync(this DbSet<User> users) => users
        .Include(u => u.Trips)
            .ThenInclude(t => t.Stops)
                .ThenInclude(s => s.Time)
        .Include(u => u.Trips)
            .ThenInclude(t => t.Stops)
                .ThenInclude(s => s.Location)
        .AsQueryable();
}