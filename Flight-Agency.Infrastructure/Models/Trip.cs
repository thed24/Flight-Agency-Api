using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Infrastructure;

public class Trip
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string Destination { get; set; }
    public IEnumerable<Stop> Stops { get; set; }

    public Trip(string destination, IEnumerable<Stop> stops)
    {
        Destination = destination;
        Stops = stops;
    }

    public Trip(string destination, IEnumerable<Stop> stops, int id)
    {
        Destination = destination;
        Stops = stops;
        Id = id;
    }

    public Trip()
    {

    }
}
