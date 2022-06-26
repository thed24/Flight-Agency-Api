namespace FlightAgency.Models;

public class Stop
{
    public int Id { get; set; }
    public int Day { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public DateRange Time { get; set; }
    public string Address { get; set; }
    public Category Category { get; set; }
}
