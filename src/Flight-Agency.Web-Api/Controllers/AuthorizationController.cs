using FlightAgency.Application;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace FlightAgency.WebApi.Controllers;

[ApiController]
[Route("/api/authorization")]
public class AuthorizationController
{
    public AuthorizationController(IAuthorizationHandler authorizationHandler)
    {
        AuthorizationHandler = authorizationHandler;
    }

    private IAuthorizationHandler AuthorizationHandler { get; }

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