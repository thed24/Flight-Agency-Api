using FlightAgency.Models.User;
using FlightAgency.Models.User.ValueObjects;

namespace FlightAgency.Contracts.Requests.Trips;

public record StopRequest(string Name, Location Location, DateRange Time, string Address, Category Category);