using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Infrastructure;

public class DateRange
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}