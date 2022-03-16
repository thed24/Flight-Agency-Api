namespace Flight_Agency_Domain;

public class Trip
{
    public int Id { get; set; }
    public string Destination { get; set; }
    public IEnumerable<Stop> Stops { get; set; }
}
