namespace FlightAgency.Models;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public List<Trip> Trips { get; private set; } = new List<Trip>();

    public User(string email, string name, string password)
    {
        Email = email;
        Password = password;
        Name = name;
    }
}
