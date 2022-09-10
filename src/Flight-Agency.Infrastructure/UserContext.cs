using System.Reflection;
using FlightAgency.Models;
using FlightAgency.Models.User;
using FlightAgency.Models.User.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FlightAgency.Infrastructure;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Stop> Stops { get; set; } = null!;
    public DbSet<DateRange> Dates { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? database = Environment.GetEnvironmentVariable("DB_NAME");
        string? user = Environment.GetEnvironmentVariable("DB_USER");
        string? password = Environment.GetEnvironmentVariable("DB_PASS");
        string? host = Environment.GetEnvironmentVariable("DB_HOST");

        string connectionString = $"Host={host};Username={user};Password={password};Database={database}";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}