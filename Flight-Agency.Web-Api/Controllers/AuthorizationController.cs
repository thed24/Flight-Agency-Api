using FlightAgency.Application.Features.Authorization.AuthorizationHandler;
using FlightAgency.Contracts.Requests.Authorization;
using FlightAgency.Models;
using Microsoft.AspNetCore.Mvc;

public class AuthorizationController
{
    public IAuthorizationHandler AuthorizationHandler { get; }

    public AuthorizationController(IAuthorizationHandler authorizationHandler)
    {
        AuthorizationHandler = authorizationHandler;
    }

    [HttpPost("api/auth/login")]
    public async Task<IResult> Login(LoginRequest loginRequest)
    {
        return (await AuthorizationHandler.LoginAsync(loginRequest)).MapToApiResponse<string, User>();
    }

    [HttpPost("api/auth/register")]
    public async Task<IResult> Register(RegisterRequest registerRequest)
    {
        return (await AuthorizationHandler.RegisterAsync(registerRequest)).MapToApiResponse<string, User>();
    }
}