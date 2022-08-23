using FlightAgency.Models;

namespace FlightAgency.Contracts.Requests.Calendar;

public record CreateIcalRequest(int UserId, int TripId);