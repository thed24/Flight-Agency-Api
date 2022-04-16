using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(service => service.AddDefaultPolicy(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader())
);

builder.Services.AddDbContext<UserContext>();
builder.Services.AddTransient<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddTransient<ITripsHandler, TripsHandler>();

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
