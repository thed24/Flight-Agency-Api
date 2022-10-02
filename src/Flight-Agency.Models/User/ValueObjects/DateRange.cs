namespace FlightAgency.Models.User.ValueObjects;

public class DateRange
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public DateRange(int id, DateTime start, DateTime end)
    {
        Id = id;
        Start = start;
        End = end;
    }
}