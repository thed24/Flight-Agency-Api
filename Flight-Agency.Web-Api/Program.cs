using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Application.Features.Trips.Requests;
using FlightAgency.Application.Features.Trips.TripHandler;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Infrastructure;
using FlightAgency.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(service => service.AddDefaultPolicy(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader())
);

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<UserContext>();
builder.Services.AddTransient<IAuthorizationHandler, AuthorizationHandler>();
builder.Services.AddTransient<ITripsHandler, TripsHandler>();

var app = builder.Build();

// auth
app.MapPost("api/auth/login", async (
    [FromServices] IAuthorizationHandler authorizationHandler,
    [FromBody] LoginRequest loginRequest) => (await authorizationHandler
        .LoginAsync(loginRequest))
        .MapToApiResponse<string, User>());

app.MapPost("api/auth/register", async (
    [FromServices] IAuthorizationHandler authorizationHandler,
    [FromBody] RegisterRequest registerRequest) => (await authorizationHandler
        .RegisterAsync(registerRequest))
        .MapToApiResponse<string, User>());

// trips
app.MapPost("api/users/{userId}/trips", async (
    [FromServices] ITripsHandler tripHandler,
    [FromBody] CreateTripRequest createTripRequest,
    [FromRoute] int userId) => (await tripHandler
        .CreateTrip(userId, createTripRequest))
        .MapToApiResponse<string, User>());

app.MapGet("api/users/{userId}/trips", async (
    [FromServices] ITripsHandler tripHandler,
    [FromRoute] int userId) => await tripHandler.GetTrips(userId));

// middleware
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
