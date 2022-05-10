namespace FlightAgency.Application.Features.Trips.Requests;

public record CreateTripRequest(
    string Destination,
    IEnumerable<StopRequest> Stops
);
