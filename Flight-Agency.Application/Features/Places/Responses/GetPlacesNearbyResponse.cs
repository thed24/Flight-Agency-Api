using FlightAgency.Infrastructure;

namespace FlightAgency.Application.Features.Places.Responses;

public record GetPlacesNearbyResponse
(
    IEnumerable<GetPlacesNearbyResponseData> Results
);

public record GetPlacesNearbyResponseData
(
    string Name, double Rating, string Icon, string ID, string Reference, string Vicinity, string[] Types, string PlaceId, Geometry Geometry
);

public record Geometry
(
    Location Location
);