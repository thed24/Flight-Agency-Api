namespace FlightAgency.Contracts.Requests.Authorization;

public record RegisterRequest(string Name, string Email, string Password);