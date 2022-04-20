using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/auth")]
public class AuthorizationController
{
    public IAuthorizationHandler AuthorizationHandler { get; }

    public AuthorizationController(IAuthorizationHandler authorizationHandler)
    {
        AuthorizationHandler = authorizationHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        return (await AuthorizationHandler
            .LoginAsync(loginRequest))
            .MapToApiResponse<string, User>();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        return (await AuthorizationHandler
            .RegisterAsync(registerRequest))
            .MapToApiResponse<string, User>();
    }
}