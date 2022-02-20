namespace Flight_Agency_Domain;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Destination { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    List<Stop> Stops { get; set; } = new List<Stop>();
    public Trip(string name, string destination, DateTime departure, DateTime arrival)
    {
        Name = name;
        Destination = destination;
        Departure = departure;
        Arrival = arrival;
    }
}
