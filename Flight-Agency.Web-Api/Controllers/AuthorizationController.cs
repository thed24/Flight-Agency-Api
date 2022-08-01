using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthorizationController
{
    private IAuthorizationHandler AuthorizationHandler { get; }

    public AuthorizationController(IAuthorizationHandler authorizationHandler)
    {
        AuthorizationHandler = authorizationHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        return (await AuthorizationHandler
            .LoginAsync(loginRequest))
            .MapToApiResponse();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        return (await AuthorizationHandler
            .RegisterAsync(registerRequest))
            .MapToApiResponse();
    }
}