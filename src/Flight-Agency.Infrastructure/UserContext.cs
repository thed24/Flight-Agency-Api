using System.Reflection;
using FlightAgency.Models;
using FlightAgency.Models.User;
using FlightAgency.Models.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FlightAgency.Infrastructure;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Stop> Stops { get; set; } = null!;
    public DbSet<DateRange> Dates { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    private readonly IOptionsMonitor<DatabaseSettings> _settings;

    public UserContext(IOptionsMonitor<DatabaseSettings> settings)
    {
        _settings = settings;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var settings = _settings.CurrentValue;
        string connectionString = $"Host={settings.Host};Username={settings.Username};Password={settings.Password};Database={settings.DatabaseName}";
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}