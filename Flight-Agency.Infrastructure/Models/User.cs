namespace FlightAgency.Infrastructure;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Trip> Trips { get; set; } = new List<Trip>();

    public User(string password, string name, string email)
    {
        Password = password;
        Name = name;
        Email = email;
    }

    public User(User oldUser, Trip newTrip)
    {
        Password = oldUser.Password;
        Name = oldUser.Name;
        Email = oldUser.Email;
        Trips = new List<Trip>() { newTrip }.Concat(oldUser.Trips).ToList();
    }

    public User()
    {

    }
}
