namespace Flight_Agency_Domain;

public class Stop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public DateRange Time { get; set; }
    public string Address { get; set; }
}

public record Location(int Id, double Latitude, double Longitude);
public record DateRange(int Id, DateTime Start, DateTime End);