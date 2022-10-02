using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgency.Models.User;

public class Trip
{
    public int Id { get; set; }
    public string Destination { get; set; } = null!;
    public List<Stop> Stops { get; set; } = new();
    [NotMapped] public Stop? FirstStop => SortedStops.FirstOrDefault();
    [NotMapped] public Stop? LastStop => SortedStops.LastOrDefault();
    [NotMapped] private List<Stop> SortedStops => Stops.OrderBy(stop => stop.Day).ThenBy(stop => stop.Time.Start).ToList();
}