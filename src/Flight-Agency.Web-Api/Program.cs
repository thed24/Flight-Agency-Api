using System.Text.Json.Serialization;
using FlightAgency.Application;
using FlightAgency.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<UserContext>(ServiceLifetime.Scoped);
builder.Services.AddScoped<ICalendarHandler, CalendarHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddScoped<ITripsHandler, TripsHandler>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

WebApplication app = builder.Build();
string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.UseHttpLogging();
app.Run();