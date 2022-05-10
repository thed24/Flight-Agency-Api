using FlightAgency.Models;

namespace FlightAgency.Application.Features.Trips.Requests;

public record StopRequest(string Name, Location Location, DateRange Time, string Address, Category Category);