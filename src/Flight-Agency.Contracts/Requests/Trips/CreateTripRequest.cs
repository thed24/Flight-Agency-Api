namespace FlightAgency.Contracts.Requests.Trips;

public record CreateTripRequest(
    string Destination,
    IEnumerable<StopRequest> Stops
);