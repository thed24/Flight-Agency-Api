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
    Console.WriteLine(ex.Message);
    key = "AIzaSyD5ALoWQXvyXXglGtNdgQbPUw9hUabxUrM";
}
var app = builder.Build();

// auth
app.MapPost("api/auth/login", async (
    [FromServices] IAuthorizationService authorizationService,
    [FromBody] LoginRequest loginRequest) =>
{
    var result = await authorizationService.Login(loginRequest);
    if (result is null) throw new Exception("Login failed");

    return result;
});

app.MapPost("api/auth/register", async (
    [FromServices] IAuthorizationService authorizationService,
    [FromBody] CreateUserRequest createUserRequest) =>
{
    var newUser = await authorizationService.Register(createUserRequest);
    if (newUser is null) throw new Exception("User already exists");

    return newUser;
});

// places
app.MapGet("api/places/nearBy", async (
    [FromQuery] double lat,
    [FromQuery] double lng,
    [FromQuery] int radius,
    [FromQuery] string keyword,
    [FromQuery] int zoom) =>
{
    var request = new PlacesNearByRequest
    {
        ApiKey = key,
        Location = new GoogleMapsApi.Entities.Common.Location(lat, lng),
        Radius = radius,
        Type = keyword,
    };
    return await GoogleMaps.PlacesNearBy.QueryAsync(request);
});

// trips
app.MapPost("api/{userId}/trips", async (
    [FromServices] ITripsService tripService,
    [FromBody] CreateTripRequest createTripRequest,
    [FromRoute] int userId) =>
{
    return await tripService.CreateTrip(userId, createTripRequest);
});

app.MapGet("api/{userId}/trips", async (
    [FromServices] ITripsService tripService,
    [FromRoute] int userId) =>
{
    return await tripService.GetTrips(userId);
});

app.MapPut("{userId}/trips", async (
    [FromServices] ITripsService tripService,
    [FromBody] UpdateTripRequest updateTripRequest,
    [FromRoute] int userId) =>
{
    return await tripService.UpdateTrip(userId, updateTripRequest);
});

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://+:{port}");

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
