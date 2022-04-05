using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Infrastructure;

public class Stop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
    public DateRange Time { get; set; }
    public string Address { get; set; }
    public Category Category { get; set; }
}
