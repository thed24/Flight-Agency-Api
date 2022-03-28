namespace FlightAgency.Application.Features.Authorization.Requests;

public record CreateUserRequest(
    string Password,
    string Name,
    string Email
);
