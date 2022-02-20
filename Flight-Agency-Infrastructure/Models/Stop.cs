namespace Flight_Agency_Domain;

public class Stop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public DateTime Arrival { get; set; }
}

public record Location(int Id, double Latitude, double Longitude);