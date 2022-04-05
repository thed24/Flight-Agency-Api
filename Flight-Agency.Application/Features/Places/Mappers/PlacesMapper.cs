using FlightAgency.Application.Features.Places.Responses;
using FlightAgency.Infrastructure;
using GoogleMapsApi.Entities.Geocoding.Response;
using GoogleMapsApi.Entities.PlacesNearBy.Response;

namespace FlightAgency.Application.Features.Places.Mappers;

public static class PlacesMapper
{
    public static GetPlacesNearbyResponse MapToResponse(this PlacesNearByResponse libraryResponse)
    {
        return new GetPlacesNearbyResponse(libraryResponse.Results.Select(result =>
            new GetPlacesNearbyResponseData(
                    Rating: result.Rating,
                    Name: result.Name,
                    Icon: result.Icon,
                    ID: result.ID,
                    Reference: result.Reference,
                    Vicinity: result.Vicinity,
                    Types: result.Types,
                    PlaceId: result.PlaceId,
                    Geometry: new Responses.Geometry(
                                new Location(
                                    result.Geometry.Location.Latitude, result.Geometry.Location.Longitude
                                ))
                )));
    }

    public static GetAddressResponse MapToResponse(this GeocodingResponse libraryResponse)
    {
        return new GetAddressResponse(libraryResponse.Results.Select(result =>
            new GetAddressResponseData(
                result.PartialMatch,
                result.FormattedAddress,
                new Responses.Geometry(
                                new Location(
                                    result.Geometry.Location.Latitude, result.Geometry.Location.Longitude
                                )))
        ));
    }
}
