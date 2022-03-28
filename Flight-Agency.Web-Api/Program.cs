using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Application.Features.Authorization.Requests;
using FlightAgency.Application.Features.Places.PlacesHandler;
using FlightAgency.Application.Features.Places.Requests;
using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.Infrastructure;
using FlightAgency.WebApi.Configs.ApiKeys;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var context = new UserContext();
context.Database.EnsureCreated();

builder.Services.AddControllers();
builder.Services.AddCors(service => service.AddDefaultPolicy(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader())
);

var keys = ProgramHelper.GetApiKeys("projects/620313617886/secrets/google-api-key");

builder.Services.AddSingleton(context);
builder.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddSingleton<ITripsHandler, TripsHandler>();
builder.Services.AddSingleton<IPlacesHandler, PlacesHandler>();
builder.Services.AddSingleton<IApiKeys>(keys);

var app = builder.Build();

// auth
app.MapPost("api/auth/login", (
    [FromServices] IAuthorizationHandler authorizationHandler,
    [FromBody] LoginRequest loginRequest) => authorizationHandler
        .Login(loginRequest)
        .MapToApiResponse<string, User>());

app.MapPost("api/auth/register", (
    [FromServices] IAuthorizationHandler authorizationHandler,
    [FromBody] CreateUserRequest createUserRequest) => authorizationHandler
        .Register(createUserRequest)
        .MapToApiResponse<string, User>());

// trips
app.MapPost("api/{userId}/trips", (
    [FromServices] ITripsHandler tripHandler,
    [FromBody] CreateTripRequest createTripRequest,
    [FromRoute] int userId) => tripHandler
        .CreateTrip(userId, createTripRequest)
        .MapToApiResponse<string, Trip>());

app.MapGet("api/{userId}/trips", (
    [FromServices] ITripsHandler tripHandler,
    [FromRoute] int userId) => tripHandler
        .GetTrips(userId)
        .MapToApiResponse<string, List<Trip>>());

app.MapPut("{userId}/trips", (
    [FromServices] ITripsHandler tripHandler,
    [FromBody] UpdateTripRequest updateTripRequest) => tripHandler
        .UpdateTrip(updateTripRequest)
        .MapToApiResponse<string, Trip>());

// places
app.MapGet("api/places/nearBy", async (
    [FromQuery] double lat,
    [FromQuery] double lng,
    [FromQuery] int radius,
    [FromQuery] string keyword,
    [FromQuery] int zoom,
    [FromServices] IPlacesHandler placesHandler) =>
{
    var request = new GetPlacesNearbyRequest(lat, lng, radius, keyword, zoom);
    return await placesHandler.GetPlacesNearbyAsync(request, keys.GoogleApiKey);
});

app.MapPost("api/places/suggest", async (
    [FromServices] IPlacesHandler placesHandler,
    [FromBody] Trip trip) => await placesHandler.GetSuggestion(trip, keys.GoogleApiKey));

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
