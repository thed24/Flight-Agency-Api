
namespace FlightAgency.Application.Features.Places.Responses;

public record GetAddressResponse
(
    IEnumerable<GetAddressResponseData> Results
);
public record GetAddressResponseData
(
    bool Match, string FormattedAddress, Geometry Geometry
);

