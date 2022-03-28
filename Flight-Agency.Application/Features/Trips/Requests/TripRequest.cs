using FlightAgency.Infrastructure;

namespace FlightAgency.Application.Features.Trips.Requests;

public record CreateTripRequest(
    string Name,
    string Destination,
    IEnumerable<Stop> Stops
);

public record UpdateTripRequest(
    int Id,
    string Name,
    string Destination,
    IEnumerable<Stop> Stops
);
