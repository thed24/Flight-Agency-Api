using FlightAgency.Application.Features.Places.Mappers;
using FlightAgency.Application.Features.Places.Requests;
using FlightAgency.Application.Features.Places.Responses;
using FlightAgency.Infrastructure;
using GoogleMapsApi;
using GoogleMapsApi.Entities.PlacesNearBy.Request;
using LanguageExt;

namespace FlightAgency.Application.Features.Places.PlacesHandler;

public interface IPlacesHandler
{
    public Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request, string key);
    public Task<IEnumerable<GetPlacesNearbyResponse>> GetSuggestion(Trip trip, string key);
}

public class PlacesHandler : IPlacesHandler
{
    public UserContext UserContext;

    public PlacesHandler(UserContext userContext)
    {
        UserContext = userContext;
    }

    public async Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request, string key)
    {
        var newRequest = new PlacesNearByRequest()
        {
            ApiKey = key,
            Radius = request.Radius,
            Keyword = request.Keyword,
            Location = new GoogleMapsApi.Entities.Common.Location(request.Lat, request.Lng)
        };

        var response = await GoogleMaps.PlacesNearBy.QueryAsync(newRequest);
        return response.MapToResponse();
    }

    public Task<IEnumerable<GetPlacesNearbyResponse>> GetSuggestion(Trip trip, string key) =>
        CalculateStops(trip, key);

    private async Task<IEnumerable<GetPlacesNearbyResponse>> CalculateStops(Trip trip, string key)
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
            ApiKey = key,
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
}