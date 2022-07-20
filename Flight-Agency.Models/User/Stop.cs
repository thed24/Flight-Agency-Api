namespace FlightAgency.Models;

public class Stop
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string Name { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public DateRange Time { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Category Category { get; set; }
}
