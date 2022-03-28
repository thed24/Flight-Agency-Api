namespace FlightAgency.Application.Features.Authorization.Requests;

public record LoginRequest(
    string Password,
    string Email
);