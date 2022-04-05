using FlightAgency.Application.Features.Places.Mappers;
using FlightAgency.Application.Features.Places.Requests;
using FlightAgency.Application.Features.Places.Responses;
using FlightAgency.Infrastructure;
using Google.Cloud.SecretManager.V1;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.PlacesNearBy.Request;
using LanguageExt;
using Microsoft.Extensions.Configuration;

namespace FlightAgency.Application.Features.Places.PlacesHandler;

public interface IPlacesHandler
{
    public Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request);
    public Task<GetAddressResponse> GetAddressAsync(GetAddressRequest request);
    public Task<IEnumerable<GetPlacesNearbyResponse>> GetSuggestion(Trip trip);
}

public class PlacesHandler : IPlacesHandler
{
    public IConfiguration Configuration;
    public UserContext UserContext;

    public PlacesHandler(UserContext userContext, IConfiguration configuration)
    {
        UserContext = userContext;
        Configuration = configuration;
    }

    public async Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request)
    {
        var newRequest = new PlacesNearByRequest()
        {
            ApiKey = GetApiKey(),
            Radius = request.Radius,
            Keyword = request.Keyword,
            Location = new GoogleMapsApi.Entities.Common.Location(request.Lat, request.Lng)
        };

        var response = await GoogleMaps.PlacesNearBy.QueryAsync(newRequest);
        return response.MapToResponse();
    }

    public async Task<GetAddressResponse> GetAddressAsync(GetAddressRequest request)
    {
        var newRequest = new GeocodingRequest()
        {
            ApiKey = GetApiKey(),
            Location = new GoogleMapsApi.Entities.Common.Location(request.Lat, request.Lng)
        };

        var response = await GoogleMaps.Geocode.QueryAsync(newRequest);
        return response.MapToResponse();
    }

    public Task<IEnumerable<GetPlacesNearbyResponse>> GetSuggestion(Trip trip) =>
        CalculateStops(trip);

    private async Task<IEnumerable<GetPlacesNearbyResponse>> CalculateStops(Trip trip)
    {
        if (trip == null || trip.Stops.Any() is false)
        {
            return new List<GetPlacesNearbyResponse>();
        }

        var groupedStops = trip.Stops.GroupBy(stop => stop.Category).OrderBy(group => group.Count()).ToList();
        var randomStop = trip.Stops.First();

        var possibleCategories = Enum.GetValues<Category>();
        var missingCategoies = possibleCategories
            .Where(category => groupedStops.All(group => group.Key != category))
            .ToList();

        var createRequest = (Category category) => new PlacesNearByRequest()
        {
            ApiKey = GetApiKey(),
            Radius = 2000,
            Keyword = category.ToString(),
            Location = new GoogleMapsApi.Entities.Common.Location(randomStop.Location.Latitude, randomStop.Location.Longitude)
        };

        var categoriesToActOn = missingCategoies.Any()
            ? missingCategoies
                .Take(3)
            : groupedStops
                .Take(3)
                .Select(group => group.Key);

        var suggestions = categoriesToActOn
                .Select(createRequest)
                .Select(request => GoogleMaps.PlacesNearBy.QueryAsync(request));

        var output = await Task.WhenAll(suggestions);
        return output.Select(x => x.MapToResponse());
    }

    public string GetApiKey()
    {
        return Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? Configuration["GoogleApiKey"];
    }
}