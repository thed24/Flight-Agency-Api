namespace FlightAgency.Models;

public class Stop
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Location Location { get; private set; }
    public DateRange Time { get; private set; }
    public string Address { get; private set; }
    public Category Category { get; private set; }

    public Stop(int id, string name, DateRange time, string address, Location location, Category category)
    {
        Id = id;
        Name = name;
        Time = time;
        Address = address;
        Location = location;
        Category = category;
    }

    public Stop() { }
}
