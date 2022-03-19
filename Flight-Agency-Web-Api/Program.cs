using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Domain;
using Google.Cloud.SecretManager.V1;
using GoogleMapsApi;
using GoogleMapsApi.Entities.PlacesNearBy.Request;
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

builder.Services.AddSingleton(context);
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<ITripsService, TripsService>();

string key;
try
{
    var client = SecretManagerServiceClient.Create();
    var result = client.AccessSecretVersion("projects/620313617886/secrets/google-api-key");
    key = result.Payload.Data.ToStringUtf8();
}
catch (Exception ex)
{
    key = "";
}
var app = builder.Build();

// auth
app.MapPost("auth/login", async (
    [FromServices] AuthorizationService authorizationService,
    LoginRequest loginRequest) =>
{
    var result = await authorizationService.Login(loginRequest);
    if (result is null) throw new Exception("Login failed");

    return result;
});

app.MapPost("auth/register", async (
    [FromServices] AuthorizationService authorizationService,
    CreateUserRequest createUserRequest) =>
{
    var newUser = await authorizationService.Register(createUserRequest);
    if (newUser is null) throw new Exception("User already exists");

    return newUser;
});

// places
app.MapPost("places/nearBy", async (
    PlacesNearByRequest placesNearByRequest) =>
{
    placesNearByRequest.ApiKey = key;
    return await GoogleMaps.PlacesNearBy.QueryAsync(placesNearByRequest);
});

// trips
app.MapPost("{userId}/trips", async (
    [FromServices] ITripsService tripService,
    CreateTripRequest createTripRequest,
    int userId) =>
{
    return await tripService.CreateTrip(userId, createTripRequest);
});

app.MapGet("{userId}/trips", async (
    [FromServices] ITripsService tripService,
    int userId) =>
{
    return await tripService.GetTrips(userId);
});

app.MapPut("{userId}/trips", async (
    [FromServices] ITripsService tripService,
    UpdateTripRequest updateTripRequest,
    int userId) =>
{
    return await tripService.UpdateTrip(userId, updateTripRequest);
});

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
