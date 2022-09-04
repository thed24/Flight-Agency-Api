using System.Net.Mime;
using FlightAgency.Application;
using FlightAgency.Contracts.Requests.Calendar;
using FlightAgency.Contracts.Requests.Trips;
using FlightAgency.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : Controller
{
    public UserController(ITripsHandler tripsHandler, ICalendarHandler calendarHandler)
    {
        TripsHandler = tripsHandler;
        CalendarHandler = calendarHandler;
    }

    private ITripsHandler TripsHandler { get; }
    private ICalendarHandler CalendarHandler { get; }

    [HttpPost("{userId}/trips")]
    public async Task<IActionResult> CreateTrip([FromRoute] int userId, [FromBody] CreateTripRequest createTripRequest)
    {
        return (await TripsHandler
                .CreateTrip(userId, createTripRequest))
            .MapToApiResponse();
    }

    [HttpGet("{userId}/trips")]
    public IActionResult GetTrips([FromRoute] int userId)
    {
        return new OkObjectResult(TripsHandler.GetTrips(userId));
    }

    [HttpPost("{userId}/trips/{tripId}/record")]
    public async Task<IActionResult> RecordTripAsync([FromRoute] int userId, [FromRoute] int tripId)
    {
        ContentDisposition cd = new()
        {
            FileName = $"trip{tripId}.ical",
            Inline = false
        };

        Response.Headers.Add("Content-Disposition", cd.ToString());

        return (await CalendarHandler.CreatIcal(new CreateIcalRequest(userId, tripId))).Match<IActionResult>(
            Left: error => new BadRequestObjectResult(error),
            Right: result => File(result, "application/force-download"));
    }
}