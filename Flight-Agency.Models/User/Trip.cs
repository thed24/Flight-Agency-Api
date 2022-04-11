namespace FlightAgency.Models;
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
    public Trip(string name, IEnumerable<Stop> stops, int id)
    {
        Stops = stops;
        Id = id;
    }
    public Trip() { }
}
