namespace FlightAgency.Application.Features.Trips.Requests;

public record UpdateTripRequest(
    int Id,
    string Destination,
    IEnumerable<StopRequest> Stops
);

