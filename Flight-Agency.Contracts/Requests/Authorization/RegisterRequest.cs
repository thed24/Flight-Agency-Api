namespace FlightAgency.Contracts.Requests.Authorization;

public record RegisterRequest(string name, string email, string password);

