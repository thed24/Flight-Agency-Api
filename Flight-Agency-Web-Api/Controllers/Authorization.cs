using Microsoft.AspNetCore.Mvc;
using Flight_Agency_Infrastructure.Common;
using Flight_Agency_Api.Features.Authorization.Services;
using Flight_Agency_Domain;

namespace Flight_Agency_Api;

public class Authorization
{
    private IAuthorizationService AuthorizationService { get; }

    public Authorization(IAuthorizationService authorizationService)
    {
        AuthorizationService = authorizationService;
    }


    [HttpPost(Name = "Login")]
    [Route("auth/login")]
    public async Task<IActionResult> Login(
        HttpRequest req,
        ILogger log)
    {
        var loginRequest = await Mapper.MapStreamToObject<LoginRequest>(req.Body);
        if (loginRequest is null) return new BadRequestResult();

        var result = await AuthorizationService.Login(loginRequest);
        if (result is null) return new BadRequestObjectResult("Login failed");

        return new OkObjectResult(result);
    }

    [HttpPost(Name = "Register")]
    [Route("auth/register")]
    public async Task<IActionResult> Register(
        HttpRequest req,
        ILogger log)
    {
        var createUserRequest = await Mapper.MapStreamToObject<CreateUserRequest>(req.Body);
        if (createUserRequest is null) return new BadRequestResult();

        var newUser = await AuthorizationService.Register(createUserRequest);
        if (newUser is null) return new BadRequestObjectResult("User already exists");

        return new OkObjectResult(newUser);
    }
}
