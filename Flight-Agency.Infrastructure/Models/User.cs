using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Infrastructure;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Trip> Trips { get; set; } = new List<Trip>();
    public User(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
    }
}
