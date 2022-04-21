namespace FlightAgency.Infrastructure;

using FlightAgency.Models;
using Microsoft.EntityFrameworkCore;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<DateRange> Dates { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var database = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASS");
        var host = Environment.GetEnvironmentVariable("DB_HOST");

        var connectionString = $"Host={host};Username={user};Password={password};Database={database}";
        optionsBuilder.UseNpgsql(connectionString);
    }
}