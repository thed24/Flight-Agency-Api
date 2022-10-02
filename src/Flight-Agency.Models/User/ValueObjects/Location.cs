namespace FlightAgency.Models.User.ValueObjects;

public class Location
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Location(int id, double latitude, double longitude)
    {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
    }
}