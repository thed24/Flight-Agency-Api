using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

    [FunctionName(nameof(Login))]
    public async Task<IActionResult> Login(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/login")]
        HttpRequest req,
        ILogger log)
    {
        var loginRequest = await Mapper.MapStreamToObject<LoginRequest>(req.Body);
        if (loginRequest is null) return new BadRequestResult();

        var result = await AuthorizationService.Login(loginRequest);
        if (result is null) return new BadRequestObjectResult("Login failed");

        return new OkObjectResult(result);
    }

    [FunctionName(nameof(Register))]
    public async Task<IActionResult> Register(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/register")]
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
