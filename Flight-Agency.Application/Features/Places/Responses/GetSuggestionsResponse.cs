namespace FlightAgency.Application.Features.Places.Responses;

public record GetSuggestionsResponse
(
    IEnumerable<GetPlacesNearbyResponseData> Results
);