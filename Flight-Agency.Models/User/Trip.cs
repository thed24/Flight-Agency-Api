namespace FlightAgency.Models;
public class Trip
{
    public int Id { get; set; }
    public string Destination { get; set; }
    public List<Stop> Stops { get; set; }
}
