namespace FlightAgency.Infrastructure;

public class Trip
{
    public int Id { get; set; }
    public string Destination { get; set; }
    public IEnumerable<Stop> Stops { get; set; }

    public Trip(string destination, IEnumerable<Stop> stops)
    {
        Destination = destination;
        Stops = stops;
    }

    public Trip(string destination, IEnumerable<Stop> stops, int id)
    {
        Destination = destination;
        Stops = stops;
        Id = id;
    }

    public Trip()
    {

    }
}
