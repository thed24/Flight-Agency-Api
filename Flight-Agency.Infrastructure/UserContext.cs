using FlightAgency.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<DateRange> Dates { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var instanceConnectionName = Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
        if (instanceConnectionName is not null)
        {
            var dbSocketDir = "/cloudsql";
            var connection = new MySqlConnectionStringBuilder()
            {
                SslMode = MySqlSslMode.None,
                Server = String.Format("{0}/{1}", dbSocketDir, instanceConnectionName),
                UserID = Environment.GetEnvironmentVariable("DB_USER"),
                Password = Environment.GetEnvironmentVariable("DB_PASS"),
                Database = Environment.GetEnvironmentVariable("DB_NAME"),
                ConnectionProtocol = MySqlConnectionProtocol.UnixSocket,
                Pooling = true,
            };

            var connectionString = connection.ConnectionString;
            var version = ServerVersion.AutoDetect(connectionString);

            optionsBuilder
                .UseMySql(connectionString, version)
                .LogTo(Console.WriteLine);
        }
        else
        {
            optionsBuilder
                .UseSqlite("Data Source=FlightAgency.db")
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var modelTypes = typeof(UserContext).GetProperties()
                         .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                         .Select(x => x.PropertyType.GetGenericArguments().First())
                         .ToList();

        foreach (Type modelType in modelTypes)
        {
            var key = modelType.GetProperties()
                               .FirstOrDefault(x => x.Name.Equals("Id", StringComparison.CurrentCultureIgnoreCase));

            if (key == null)
            {
                continue;
            }

            modelBuilder.Entity(modelType)
                        .Property(key.Name)
                        .UseMySqlIdentityColumn()
                        .IsRequired();
        }
    }
}