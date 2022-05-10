namespace FlightAgency.Models;
public class Trip
{
    public int Id { get; private set; }
    public string Destination { get; private set; }
    public IEnumerable<Stop> Stops { get; private set; }

    public Trip(string destination, IEnumerable<Stop> stops)
    {
        Destination = destination;
        Stops = stops;
    }

    public Trip() { }
}
