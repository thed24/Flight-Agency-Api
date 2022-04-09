﻿using FlightAgency.Application.Features.Places.Mappers;
using FlightAgency.Application.Features.Places.Requests;
using FlightAgency.Application.Features.Places.Responses;
using FlightAgency.Infrastructure;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.PlacesNearBy.Request;
using LanguageExt;
using Microsoft.Extensions.Caching.Memory;

namespace FlightAgency.Application.Features.Places.PlacesHandler;

public interface IPlacesHandler
{
    public Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request);
    public Task<GetAddressResponse> GetAddressAsync(GetAddressRequest request);
    public Task<GetSuggestionsResponse> GetSuggestion(Trip trip);
}

public class PlacesHandler : IPlacesHandler
{
    public UserContext UserContext;

    public IMemoryCache Cache;

    private string CreatePlaceCacheKey(double lat, double lng) => $"place_{lat}_{lng}";
    private string CreateAddressCacheKey(double lat, double lng) => $"address_{lat}_{lng}";
    private string GetApiKey() => Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

    public PlacesHandler(UserContext userContext, IMemoryCache memoryCache)
    {
        UserContext = userContext;
        Cache = memoryCache;
    }

    public async Task<GetPlacesNearbyResponse> GetPlacesNearbyAsync(GetPlacesNearbyRequest request)
    {
        var cacheKeyForRequest = CreatePlaceCacheKey(request.Lat, request.Lng);

        if (Cache.TryGetValue<GetPlacesNearbyResponse>(cacheKeyForRequest, out var places))
        {
            return places;
        }

        var newRequest = new PlacesNearByRequest()
        {
            ApiKey = GetApiKey(),
            Radius = request.Radius,
            Keyword = request.Keyword,
            Location = new GoogleMapsApi.Entities.Common.Location(request.Lat, request.Lng)
        };

        var response = await GoogleMaps.PlacesNearBy.QueryAsync(newRequest);

        Cache.Set(cacheKeyForRequest, response, TimeSpan.FromDays(7));

        return response.MapToResponse();
    }

    public async Task<GetAddressResponse> GetAddressAsync(GetAddressRequest request)
    {
        var cacheKeyForRequest = CreateAddressCacheKey(request.Lat, request.Lng);

        if (Cache.TryGetValue<GetAddressResponse>(cacheKeyForRequest, out var address))
        {
            return address;
        }

        var newRequest = new GeocodingRequest()
        {
            ApiKey = GetApiKey(),
            Location = new GoogleMapsApi.Entities.Common.Location(request.Lat, request.Lng)
        };

        var response = await GoogleMaps.Geocode.QueryAsync(newRequest);

        Cache.Set(cacheKeyForRequest, response, TimeSpan.FromDays(7));

        return response.MapToResponse();
    }

    public async Task<GetSuggestionsResponse> GetSuggestion(Trip trip)
    {
        if (!trip.Stops.Any())
        {
            return new GetSuggestionsResponse(new List<GetPlacesNearbyResponseData>());
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
        var data = output
            .Select(x => x.MapToResponse())
            .Select(x => x.Results)
            .SelectMany(x => x)
            .Select(x => x);

        return new GetSuggestionsResponse(data);
    }
}