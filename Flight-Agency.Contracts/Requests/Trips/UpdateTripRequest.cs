namespace FlightAgency.Contracts.Requests.Trips;

public record UpdateTripRequest(
    int Id,
    string Destination,
    IEnumerable<StopRequest> Stops
);