using System.Text;
using FlightAgency.Contracts.Requests.Calendar;
using FlightAgency.Infrastructure;
using FlightAgency.Models.User;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static LanguageExt.Prelude;

namespace FlightAgency.Application;

public interface ICalendarHandler
{
    public Task<Either<string, Stream>> CreatIcal(CreateIcalRequest request);
}

public class CalendarHandler : ICalendarHandler
{
    private readonly ILogger Logger;
    private readonly UserContext UserContext;

    public CalendarHandler(UserContext userContext, ILogger<CalendarHandler> logger)
    {
        UserContext = userContext;
        Logger = logger;
    }

    public async Task<Either<string, Stream>> CreatIcal(CreateIcalRequest request)
    {
        User? user = await UserContext.Users.IncludeAllAsync().FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null)
        {
            Logger.LogError($"User with id {request.UserId} was not found.");
            return Left<string, Stream>("User not found.");
        }

        Trip? trip = user.Trips.FirstOrDefault(t => t.Id == request.TripId);

        if (trip is null || trip.FirstStop is null || trip.LastStop is null)
        {
            Logger.LogError($"Trip with id {request.TripId} was empty not found.");
            return Left<string, Stream>("Trip was empty or not found.");
        }

        Calendar calendar = new();
        trip.Stops.ForEach(s =>
        {
            calendar.Events.Add(new CalendarEvent
            {
                Start = new CalDateTime(s.Time.Start),
                End = new CalDateTime(s.Time.End),
                Summary = s.Name,
                Categories = new[] { s.Category.ToString() },
                LastModified = new CalDateTime(DateTime.Now),
                GeographicLocation =
                    new GeographicLocation(trip.FirstStop.Location.Latitude, trip.FirstStop.Location.Longitude)
            });
        });

        CalendarSerializer serializer = new();
        string serializedCalendar = serializer.SerializeToString(calendar);

        return Right<string, Stream>(new MemoryStream(Encoding.UTF8.GetBytes(serializedCalendar)));
    }
}