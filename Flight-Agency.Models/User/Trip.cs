using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Models;

public class Trip
{
    public int Id { get; set; }
    public string Destination { get; set; } = null!;
    public List<Stop> Stops { get; set; } = new List<Stop>();
    [NotMapped]
    public int Length => Stops.Count > 0 ? Stops.Last().Day - Stops.First().Day : 0;
    [NotMapped]
    public List<Stop> SortedStops => Stops.OrderBy(stop => stop.Day).ThenBy(stop => stop.Time.Start).ToList();
    [NotMapped]
    public Stop? FirstStop => SortedStops.FirstOrDefault();
    [NotMapped]
    public Stop? LastStop => SortedStops.LastOrDefault();
}
