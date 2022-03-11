namespace Flight_Agency_Domain;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Trip? Trip { get; set; }
    public int Id { get; set; }
    public User(string password, string name, string email)
    {
        Password = password;
        Name = name;
        Email = email;
    }
}
