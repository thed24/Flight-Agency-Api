using System;

namespace Flight_Agency_Domain
{
    public record CreateTripRequest(
        string Name,
        string Destination,
        DateTime Departure,
        DateTime Arrival
    );
    public record UpdateTripRequest(
        int Id,
        string Name,
        string Destination,
        DateTime Departure,
        DateTime Arrival
    );
}