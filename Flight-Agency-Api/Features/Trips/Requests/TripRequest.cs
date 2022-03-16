using System.Collections.Generic;

namespace Flight_Agency_Domain
{
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
}